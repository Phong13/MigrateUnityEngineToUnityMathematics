// Program.cs
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Build.Locator;

namespace UnityVector3Refactor
{
    class MigrateToUnityMathematics
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Unity Vector3.magnitude Refactor Tool");
            Console.WriteLine("------------------------------------");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: UnityVector3Refactor <path_to_unity_project_or_solution_file>");
                Console.WriteLine("Example: UnityVector3Refactor \"C:\\MyUnityProject\\MyUnityProject.sln\"");
                Console.WriteLine("Or: UnityVector3Refactor \"C:\\MyUnityProject\\Assets\\Scripts\"");
                return;
            }

            MSBuildLocator.RegisterDefaults(); // Registers installed MSBuild instance

            string targetPath = args[0];
            string fullPath = Path.GetFullPath(targetPath);

            if (!File.Exists(fullPath) && !Directory.Exists(fullPath))
            {
                Console.WriteLine($"Error: Path '{fullPath}' does not exist.");
                return;
            }

            Console.WriteLine($"Processing: {fullPath}");

            var workspace = MSBuildWorkspace.Create();

            // Optional: Handle MSBuild warnings/errors
            workspace.WorkspaceFailed += (sender, e) =>
            {
                Console.WriteLine($"MSBuild Error: {e.Diagnostic.Message}");
            };

            Solution solution = null;
            if (File.Exists(fullPath) && (fullPath.EndsWith(".sln", StringComparison.OrdinalIgnoreCase) || fullPath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)))
            {
                if (fullPath.EndsWith(".sln", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Loading solution...");
                    solution = await workspace.OpenSolutionAsync(fullPath);
                }
                else // .csproj
                {
                    Console.WriteLine("Loading project...");
                    var project = await workspace.OpenProjectAsync(fullPath);
                    solution = project.Solution;
                }
            }
            else if (Directory.Exists(fullPath))
            {
                Console.WriteLine("Searching for .csproj files in directory...");
                var csprojFiles = Directory.EnumerateFiles(fullPath, "*.csproj", SearchOption.AllDirectories)
                                           .Where(f => !f.Contains("obj") && !f.Contains("bin")) // Exclude build artifacts
                                           .ToList();

                if (csprojFiles.Any())
                {
                    Console.WriteLine($"Found {csprojFiles.Count} project(s). Loading the first one found: {csprojFiles.First()}");
                    var project = await workspace.OpenProjectAsync(csprojFiles.First());
                    solution = project.Solution;
                }
                else
                {
                    Console.WriteLine("No .csproj files found in the specified directory. Please provide a solution or project file, or a directory containing .csproj files.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid target path. Please provide a path to a .sln, .csproj, or a directory.");
                return;
            }

            if (solution == null)
            {
                Console.WriteLine("Failed to load solution/project.");
                return;
            }

            Console.WriteLine($"Solution loaded. Contains {solution.Projects.Count()} project(s).");

            int totalReplacements = 0;
            foreach (var project in solution.Projects)
            {
                Console.WriteLine($"\nProcessing project: {project.Name}");
                if (project.Language != LanguageNames.CSharp)
                {
                    Console.WriteLine("Skipping non-C# project.");
                    continue;
                }

                foreach (var document in project.Documents)
                {
                    if (document.SourceCodeKind == SourceCodeKind.Regular && document.FilePath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"  Processing document: {Path.GetFileName(document.FilePath)}");
                        var originalSyntaxTree = await document.GetSyntaxTreeAsync();
                        var originalSemanticModel = await document.GetSemanticModelAsync();

                        var rewriter = new MagnitudeRewriter(originalSemanticModel);
                        var newSyntaxRoot = rewriter.Visit(originalSyntaxTree.GetRoot());

                        if (newSyntaxRoot != originalSyntaxTree.GetRoot())
                        {
                            // Document has been changed, update it in the solution
                            solution = solution.WithDocumentSyntaxRoot(document.Id, newSyntaxRoot);
                            Console.WriteLine($"    Replaced {rewriter.ReplacementsCount} instances in {Path.GetFileName(document.FilePath)}");
                            totalReplacements += rewriter.ReplacementsCount;
                        }
                    }
                }
            }

            Console.WriteLine("\nApplying changes to disk...");
            if (workspace.TryApplyChanges(solution))
            {
                Console.WriteLine($"Successfully applied all changes. Total replacements: {totalReplacements}");
                Console.WriteLine("Remember to add the Vector3Utils.cs file to your Unity project if you haven't already.");
            }
            else
            {
                Console.WriteLine("Failed to apply changes to disk.");
            }
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.magnitude' property access
    /// with 'Vector3Utils.length(vector)'.
    /// </summary>
    class MagnitudeRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public MagnitudeRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        // Override VisitMemberAccessExpression to find property accesses
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            // First, let the base rewriter visit the children to ensure inner expressions are processed
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            // Check if the member being accessed is "magnitude"
            if (newNode.Name.Identifier.Text == "magnitude")
            {
                // Get the symbol for the member access
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                // Check if it's a property and if its containing type is Unity's Vector3
                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    // This is a Vector3.magnitude access!
                    ReplacementsCount++;

                    // Get the expression that the magnitude is being called on (e.g., 'myVector' in 'myVector.magnitude')
                    var expression = newNode.Expression;

                    // Create the new method call: Vector3Utils.length(expression)
                    // First, the argument list for the new method call
                    var argumentList = SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Argument(expression.WithoutTrivia()) // Remove trivia from the expression for clean insertion
                        )
                    );

                    // Create the member access for Vector3Utils.length
                    var memberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3Utils"), // The class name
                        SyntaxFactory.IdentifierName("length")       // The static method name
                    );

                    // Create the invocation expression
                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        memberAccess,
                        argumentList
                    );

                    // Preserve leading trivia from the original member access expression
                    // and trailing trivia from the original node.
                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode; // Return the (potentially modified) node
        }
    }
}