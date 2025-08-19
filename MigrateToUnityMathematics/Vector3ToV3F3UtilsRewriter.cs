// Program.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace UnityVector3Refactor
{
    class Vector3ToV3F3UtilsRewriter
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

            /*
            MSBuildLocator.RegisterDefaults(); // Registers installed MSBuild instance

            ^This was not working for me, replaced with RegisterMSBuildPath() and used:
            dotnet run --project MigrateToUnityMathematics.csproj -- "C:\Users\rowan\Workspace\Unity\Vector3ToFloat3MigrationTestingUnity\Assembly-CSharp.csproj"
            */

            const string msBuildPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin";
            try
            {
                MSBuildLocator.RegisterMSBuildPath(msBuildPath);
                Console.WriteLine($"Successfully registered MSBuild from custom path: {msBuildPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering MSBuild path: {ex.Message}");
                Console.WriteLine("Please ensure the path is correct and that Visual Studio is installed with the MSBuild workload.");
                return;
            }

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
                    if (document.SourceCodeKind == SourceCodeKind.Regular &&
                        document.FilePath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) &&
                        !document.Name.Contains("Vector3ToFloat3Utils")) // Make sure not to change the file with the wrappers
                    {
                        Console.WriteLine($"  Processing document: {Path.GetFileName(document.FilePath)}");

                        var originalSyntaxTree = await document.GetSyntaxTreeAsync();
                        var originalSemanticModel = await document.GetSemanticModelAsync();

                        var vector3ToFloat3Rewriter = new Vector3ToV3F3UtilsTransformer(originalSemanticModel);
                        var newSyntaxRoot = vector3ToFloat3Rewriter.Visit(originalSyntaxTree.GetRoot());

                        if (newSyntaxRoot != originalSyntaxTree.GetRoot())
                        {
                            totalReplacements += vector3ToFloat3Rewriter.ReplacementsCount;
                            solution = solution.WithDocumentSyntaxRoot(document.Id, newSyntaxRoot);
                            Console.WriteLine($"    Replaced " + $"{totalReplacements} instances in {Path.GetFileName(document.FilePath)}");
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
    /// Rewrites Vector3 properties, methods, and operators
    /// </summary>
    class Vector3ToV3F3UtilsTransformer : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public Vector3ToV3F3UtilsTransformer(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        /// <summary>
        /// Override VisitMemberAccessExpression for property access
        /// </summary>
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            // Get the original symbol early so we have it even if the syntax tree changes (e.g. Vector3.Angle(Vector3.up, ...))
            var originalSymbol = _semanticModel.GetSymbolInfo(node).Symbol;

            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            string memberName = newNode.Name.Identifier.Text;

            if (memberName == "magnitude" || memberName == "sqrMagnitude" || memberName == "normalized")
            {
                if (originalSymbol is IPropertySymbol propSymbol &&
                    propSymbol.ContainingType != null &&
                    propSymbol.ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                    propSymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var expression = newNode.Expression;

                    var argumentList = SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Argument(expression.WithoutTrivia())
                        )
                    );

                    var memberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName(memberName)
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        memberAccess,
                        argumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }

            if (memberName == "forward" || memberName == "back" || memberName == "up" || memberName == "down" ||
                memberName == "right" || memberName == "left" || memberName == "one" || memberName == "zero")
            {
                if (originalSymbol is IPropertySymbol propSymbol &&
                    propSymbol.ContainingType != null &&
                    propSymbol.ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                    propSymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName(memberName)
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }

            return newNode;
        }

        /// <summary>
        /// Override VisitBinaryExpression for operator access
        /// </summary>
        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            var newNode = (BinaryExpressionSyntax)base.VisitBinaryExpression(node);

            if (newNode.Kind() == SyntaxKind.EqualsExpression || newNode.Kind() == SyntaxKind.NotEqualsExpression)
            {
                var leftType = _semanticModel.GetTypeInfo(newNode.Left).Type;
                var rightType = _semanticModel.GetTypeInfo(newNode.Right).Type;

                if (leftType?.ToDisplayString() == "UnityEngine.Vector3" &&
                    rightType?.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

                    string newNodeName;

                    if (newNode.Kind() == SyntaxKind.EqualsExpression)
                    {
                        newNodeName = "equals";
                    }
                    else
                    {
                        newNodeName = "notEquals";
                    }

                    var argumentList = SyntaxFactory.ArgumentList(
                        SyntaxFactory.SeparatedList(new[]
                        {
                            SyntaxFactory.Argument(newNode.Left),
                            SyntaxFactory.Argument(newNode.Right)
                        })
                    );

                    var memberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName(newNodeName)
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        memberAccess,
                        argumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }

            return newNode;
        }

        /// <summary>
        /// Override VisitInvocationExpression for method access
        /// </summary>
        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (node.Expression is MemberAccessExpressionSyntax originalMemberAccess)
            {
                var originalSymbol = _semanticModel.GetSymbolInfo(originalMemberAccess).Symbol;

                string originalName = originalMemberAccess.Name.Identifier.Text;

                var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

                if (newNode.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    string memberAccessName = memberAccess.Name.Identifier.Text;

                    if ((originalSymbol is IMethodSymbol methodSymbol &&
                        methodSymbol.ContainingType != null &&
                        methodSymbol.ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                        methodSymbol.Parameters.Length == 2 &&
                        methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                        methodSymbol.Parameters[1].Type.ToDisplayString() == "UnityEngine.Vector3") &&
                        (memberAccessName == "Angle" || memberAccessName == "Cross" || memberAccessName == "Distance" || memberAccessName == "Dot" ||
                         memberAccessName == "Max" || memberAccessName == "Min" || memberAccessName == "Project" || memberAccessName == "ProjectOnPlane"))
                    {
                        ReplacementsCount++;

                        string newName;

                        if (originalName == "Angle")
                        {
                            newName = "Angle_deg";
                        }
                        else
                        {
                            newName = originalName;
                        }

                        var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                            SyntaxFactory.IdentifierName(newName)
                        );

                        var invocationExpression = SyntaxFactory.InvocationExpression(
                            newMemberAccess,
                            newNode.ArgumentList
                        );

                        return invocationExpression
                            .WithLeadingTrivia(newNode.GetLeadingTrivia())
                            .WithTrailingTrivia(newNode.GetTrailingTrivia());
                    }

                    if ((originalName == "Lerp" || originalName == "MoveTowards") &&
                        originalSymbol is IMethodSymbol methodSymbol2 &&
                        methodSymbol2.ContainingType != null &&
                        methodSymbol2.ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                        methodSymbol2.Parameters.Length == 3 &&
                        methodSymbol2.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                        methodSymbol2.Parameters[1].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                        methodSymbol2.Parameters[2].Type.SpecialType == SpecialType.System_Single)
                    {
                        ReplacementsCount++;

                        var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                            SyntaxFactory.IdentifierName(originalName)
                        );

                        var invocationExpression = SyntaxFactory.InvocationExpression(
                            newMemberAccess,
                            newNode.ArgumentList
                        );

                        return invocationExpression
                            .WithLeadingTrivia(newNode.GetLeadingTrivia())
                            .WithTrailingTrivia(newNode.GetTrailingTrivia());
                    }

                    if (originalName == "Normalize" &&
                        originalSymbol is IMethodSymbol methodSymbol3 &&
                        methodSymbol3.ContainingType != null &&
                        methodSymbol3.ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                        methodSymbol3.Parameters.Length == 1 &&
                        methodSymbol3.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3")
                    {
                        ReplacementsCount++;

                        var originalArg = node.ArgumentList.Arguments[0];

                        var argumentList = SyntaxFactory.ArgumentList(
                            SyntaxFactory.SingletonSeparatedList(originalArg)
                        );

                        var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                            SyntaxFactory.IdentifierName("Normalize")
                        );

                        var invocationExpression = SyntaxFactory.InvocationExpression(
                            newMemberAccess,
                            argumentList
                        );

                        return invocationExpression
                            .WithLeadingTrivia(newNode.GetLeadingTrivia())
                            .WithTrailingTrivia(newNode.GetTrailingTrivia());
                    }
                }

                return newNode;
            }

            return base.VisitInvocationExpression(node);
        }
    }
}