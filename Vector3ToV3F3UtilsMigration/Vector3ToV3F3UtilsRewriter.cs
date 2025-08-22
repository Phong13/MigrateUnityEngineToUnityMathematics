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

namespace MigrateToUnityMathematics
{
    class Vector3ToV3F3UtilsRewriter
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Unity Vector3 Refactor Tool");
            Console.WriteLine("------------------------------------");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: Vector3ToV3F3UtilsMigration <path_to_unity_project_or_solution_file> [optional: <path_to_config_file>]");
                Console.WriteLine("Example with config file: dotnet run --project Vector3ToV3F3UtilsMigration.csproj " +
                                  "-- \"C:\\Users\\rowan\\Workspace\\Unity\\Vector3ToFloat3UtilsTesting\\Assembly-CSharp.csproj\" " +
                                  "C:\\Users\\rowan\\Workspace\\Unity\\MigrateUnityEngineToUnityMathematics\\config.txt\"");
                Console.WriteLine("dotnet run --project Vector3ToV3F3UtilsMigration.csproj " +
                                  "-- \"C:\\Users\\rowan\\Workspace\\Unity\\Vector3ToFloat3UtilsTesting\\Assembly-CSharp.csproj\" ");
                return;
            }

            /*
            MSBuildLocator.RegisterDefaults(); // Registers installed MSBuild instance

            ^This was not working for me, replaced with RegisterMSBuildPath()
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

            AllowedNamespacesAndExcludedFilesFinder config;
            if (args.Length > 1)
            {
                string configPath = Path.GetFullPath(args[1]);
                config = new AllowedNamespacesAndExcludedFilesFinder(configPath);

                Console.WriteLine("Excluded File Names:");
                Console.WriteLine("  " + string.Join(", ", config.ExcludedFileNames));

                Console.WriteLine("Allowed Namespaces:");
                Console.WriteLine("  " + string.Join(", ", config.AllowedNamespaces));
            }
            else
            {
                Console.WriteLine("No config file specified. Using all namespaces, excluding no files");
                config = new AllowedNamespacesAndExcludedFilesFinder(null);
            }

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
                    if (document.SourceCodeKind != SourceCodeKind.Regular ||
                        !document.FilePath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) ||
                        document.Name.Contains("Vector3ToFloat3Utils") ||
                        document.Name.Contains("QuaternionToMathematicsUtils"))
                    {
                        continue;
                    }

                    var root = await document.GetSyntaxRootAsync();
                    var namespaceNode = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
                    string ns = "";
                    if (namespaceNode != null)
                    {
                        ns = namespaceNode.Name.ToString();
                    }

                    if (config.IsFileExcluded(document.Name))
                    {
                        Console.WriteLine($"  Did not process document: {Path.GetFileName(document.FilePath)} - File Name excluded");
                        continue;
                    }
                    if (!config.IsNamespaceAllowed(ns))
                    {
                        Console.WriteLine($"  Did not process document: {Path.GetFileName(document.FilePath)} - Namespace ({ns}) not included");
                        continue;
                    }

                    Console.WriteLine($"  Processing document: {Path.GetFileName(document.FilePath)}");

                    var originalSyntaxTree = await document.GetSyntaxTreeAsync();
                    var originalSemanticModel = await document.GetSemanticModelAsync();

                    var Vector3ToV3F3UtilsRewriter = new Vector3ToV3F3UtilsTransformer(originalSemanticModel);
                    var newSyntaxRoot = Vector3ToV3F3UtilsRewriter.Visit(originalSyntaxTree.GetRoot());

                    if (newSyntaxRoot != originalSyntaxTree.GetRoot())
                    {
                        totalReplacements += Vector3ToV3F3UtilsRewriter.ReplacementsCount;
                        solution = solution.WithDocumentSyntaxRoot(document.Id, newSyntaxRoot);
                        Console.WriteLine($"    Replaced {Vector3ToV3F3UtilsRewriter.ReplacementsCount} instances in {Path.GetFileName(document.FilePath)}");
                    }
                }
            }

            Console.WriteLine("\nApplying changes to disk...");
            if (workspace.TryApplyChanges(solution))
            {
                Console.WriteLine($"Successfully applied all changes. Total replacements: {totalReplacements}");
                Console.WriteLine("Remember to add the Vector3ToFloat3Utils.cs file to your Unity project if you haven't already.");
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

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            // Get the original symbol early so we have it even if the syntax tree changes (e.g. Vector3.Angle(Vector3.up, ...))
            var originalSymbol = _semanticModel.GetSymbolInfo(node).Symbol;

            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            string memberName = newNode.Name.Identifier.Text;

            if (originalSymbol is IPropertySymbol propSymbol &&
            propSymbol.ContainingType != null &&
            propSymbol.ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
            propSymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
            {
                if (memberName == "magnitude" || memberName == "sqrMagnitude" || memberName == "normalized")
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
                        argumentList.NormalizeWhitespace()
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }

                if (memberName == "forward" || memberName == "back" || memberName == "up" || memberName == "down" ||
                    memberName == "right" || memberName == "left" || memberName == "one" || memberName == "zero")
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

        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            var originalLeftType = _semanticModel.GetTypeInfo(node.Left).Type;
            var originalRightType = _semanticModel.GetTypeInfo(node.Right).Type;
            var originalKind = node.Kind();

            var newNode = (BinaryExpressionSyntax)base.VisitBinaryExpression(node);

            if (originalKind == SyntaxKind.EqualsExpression || originalKind == SyntaxKind.NotEqualsExpression)
            {
                if (originalLeftType?.ToDisplayString() == "UnityEngine.Vector3" &&
                    originalRightType?.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

                    string newNodeName;
                    if (originalKind == SyntaxKind.EqualsExpression)
                    {
                        newNodeName = "Equals";
                    }
                    else
                    {
                        newNodeName = "NotEquals";
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
                        argumentList.NormalizeWhitespace()
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }

            return newNode;
        }

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

                    if(originalSymbol is IMethodSymbol methodSymbol &&
                       methodSymbol.ContainingType != null &&
                       methodSymbol.ContainingType.ToDisplayString() == "UnityEngine.Vector3") 
                    { 
                        if (methodSymbol.Parameters.Length == 2 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            methodSymbol.Parameters[1].Type.ToDisplayString() == "UnityEngine.Vector3" &&
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
                                newNode.ArgumentList.NormalizeWhitespace()
                            );

                            return invocationExpression
                                .WithLeadingTrivia(newNode.GetLeadingTrivia())
                                .WithTrailingTrivia(newNode.GetTrailingTrivia());
                        }

                        if (methodSymbol.Parameters.Length == 3 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            methodSymbol.Parameters[1].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            methodSymbol.Parameters[2].Type.SpecialType == SpecialType.System_Single &&
                            (originalName == "Lerp" || originalName == "MoveTowards"))
                        {
                            ReplacementsCount++;

                            var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                                SyntaxFactory.IdentifierName(originalName)
                            );

                            var invocationExpression = SyntaxFactory.InvocationExpression(
                                newMemberAccess,
                                newNode.ArgumentList.NormalizeWhitespace()
                            );

                            return invocationExpression
                                .WithLeadingTrivia(newNode.GetLeadingTrivia())
                                .WithTrailingTrivia(newNode.GetTrailingTrivia());
                        }

                        if (methodSymbol.Parameters.Length == 1 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            methodSymbol.IsStatic &&
                            originalName == "Normalize")
                        {
                            ReplacementsCount++;

                            var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                                SyntaxFactory.IdentifierName("Normalize")
                            );

                            var invocationExpression = SyntaxFactory.InvocationExpression(
                                newMemberAccess,
                                newNode.ArgumentList.NormalizeWhitespace()
                            );

                            return invocationExpression
                                .WithLeadingTrivia(newNode.GetLeadingTrivia())
                                .WithTrailingTrivia(newNode.GetTrailingTrivia());
                        }
                        if (methodSymbol.ContainingType.ToString() == "UnityEngine.Vector3" && 
                            methodSymbol.Parameters.Length == 0 &&
                            !methodSymbol.IsStatic &&
                            originalName == "Normalize")
                        {
                            ReplacementsCount++;

                            var instanceExpr = ((MemberAccessExpressionSyntax)newNode.Expression).Expression;

                            var refArg = SyntaxFactory.Argument(instanceExpr)
                                .WithRefKindKeyword(SyntaxFactory.Token(SyntaxKind.RefKeyword));

                            var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                                SyntaxFactory.IdentifierName("Normalize")
                            );

                            var invocationExpression = SyntaxFactory.InvocationExpression(
                                newMemberAccess,
                                SyntaxFactory.ArgumentList(SyntaxFactory.SingletonSeparatedList(refArg)).NormalizeWhitespace()
                            );

                            return invocationExpression
                                .WithLeadingTrivia(newNode.GetLeadingTrivia())
                                .WithTrailingTrivia(newNode.GetTrailingTrivia());
                        }
                    }
                }

                return newNode;
            }

            return base.VisitInvocationExpression(node);
        }
    }

    public class AllowedNamespacesAndExcludedFilesFinder
    {
        public HashSet<string> AllowedNamespaces { get; } = new();
        public HashSet<string> ExcludedFileNames { get; } = new();

        public AllowedNamespacesAndExcludedFilesFinder(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Config file '{filePath}' not found. Using defaults (all namespaces allowed, no excluded files).");
                return;
            }

            var lines = File.ReadAllLines(filePath).Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l) && !l.StartsWith("#")).ToList();

            bool readingAllowedNamespaces = false;
            bool readingExcludedFileNames = false;

            foreach (var line in lines)
            {
                if (line.Equals("ALLOWED_NAMESPACES:", StringComparison.OrdinalIgnoreCase))
                {
                    readingAllowedNamespaces = true;
                    readingExcludedFileNames = false;
                    continue;
                }

                if (line.Equals("EXCLUDED_FILENAMES:", StringComparison.OrdinalIgnoreCase))
                {
                    readingAllowedNamespaces = false;
                    readingExcludedFileNames = true;
                    continue;
                }

                if (readingAllowedNamespaces)
                    AllowedNamespaces.Add(line);
                else if (readingExcludedFileNames)
                    ExcludedFileNames.Add(line);
            }
        }
        public bool IsNamespaceAllowed(string ns)
        {
            return AllowedNamespaces.Count == 0 || AllowedNamespaces.Contains(ns);
        }
        public bool IsFileExcluded(string fileName)
        {
            return ExcludedFileNames.Contains(fileName);
        }
    }
}