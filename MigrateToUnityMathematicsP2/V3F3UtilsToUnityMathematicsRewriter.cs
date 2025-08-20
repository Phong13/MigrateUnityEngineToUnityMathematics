using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
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
            Console.WriteLine("Unity Vector3 Refactor Tool");
            Console.WriteLine("------------------------------------");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: Vector3ToFloat3MigrationP2 <path_to_unity_project_or_solution_file> [optional: <path_to_config_file>]");
                Console.WriteLine("Example with config file: dotnet run --project Vector3ToFloat3MigrationP2.csproj " +
                                  "-- \"C:\\Users\\rowan\\Workspace\\Unity\\Vector3ToFloat3UtilsTesting\\Assembly-CSharp.csproj\" " +
                                  "\"C:\\Users\\rowan\\Workspace\\Unity\\MigrateUnityEngineToUnityMathematics\\MigrateToUnityMathematicsP2\\config.txt\"");
                Console.WriteLine("dotnet run --project Vector3ToFloat3MigrationP2.csproj " +
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
                        document.Name.Contains("Vector3ToFloat3Utils"))
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

                    var V3F3UtilsToUnityMathematicsRewriter = new V3F3UtilsToUnityMathematicsTransformer(originalSemanticModel);
                    var newSyntaxRoot = V3F3UtilsToUnityMathematicsRewriter.Visit(originalSyntaxTree.GetRoot());

                    if (newSyntaxRoot != originalSyntaxTree.GetRoot())
                    {
                        totalReplacements += V3F3UtilsToUnityMathematicsRewriter.ReplacementsCount;
                        solution = solution.WithDocumentSyntaxRoot(document.Id, newSyntaxRoot);
                        Console.WriteLine($"    Replaced {V3F3UtilsToUnityMathematicsRewriter.ReplacementsCount} instances in {Path.GetFileName(document.FilePath)}");
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
    /// Rewrites simple Vector3ToFloat3Utils properties, methods, and operators as their underlying logic
    /// </summary>
    class V3F3UtilsToUnityMathematicsTransformer : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public V3F3UtilsToUnityMathematicsTransformer(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var originalSymbol = _semanticModel.GetSymbolInfo(node).Symbol;

            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (originalSymbol is IPropertySymbol propSymbol &&
                propSymbol.ContainingType != null &&
                propSymbol.ContainingType.ToDisplayString() == "Vector3ToFloat3Utils")
            {
                string name = propSymbol.Name;
                ExpressionSyntax replacement = null;

                if (name == "forward") replacement = ReplaceStaticProperties(newNode, 0, 0, 1);
                else if (name == "back") replacement = ReplaceStaticProperties(newNode, 0, 0, -1);
                else if (name == "up") replacement = ReplaceStaticProperties(newNode, 0, 1, 0);
                else if (name == "down") replacement = ReplaceStaticProperties(newNode, 0, -1, 0);
                else if (name == "right") replacement = ReplaceStaticProperties(newNode, 1, 0, 0);
                else if (name == "left") replacement = ReplaceStaticProperties(newNode, -1, 0, 0);
                else if (name == "one") replacement = ReplaceStaticProperties(newNode, 1, 1, 1);
                else if (name == "zero") replacement = ReplaceStaticProperties(newNode, 0, 0, 0);

                if (replacement != null)
                {
                    ReplacementsCount++;
                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }

            return newNode;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            ISymbol originalSymbol = null;

            if (node.Expression is MemberAccessExpressionSyntax originalMemberAccess)
            {
                originalSymbol = _semanticModel.GetSymbolInfo(originalMemberAccess).Symbol;
            }

            var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

            if (originalSymbol is IMethodSymbol methodSymbol &&
                methodSymbol.ContainingType != null &&
                methodSymbol.ContainingType.ToDisplayString() == "Vector3ToFloat3Utils")
            {
                string name = methodSymbol.Name;
                var argumentList = newNode.ArgumentList;

                ExpressionSyntax replacement = null;

                if (name == "magnitude") replacement = ReplaceMethods("length", argumentList);
                else if (name == "sqrMagnitude") replacement = ReplaceMethods("lengthsq", argumentList);
                else if (name == "normalized") replacement = ReplaceMethods("normalize", argumentList);
                else if (name == "Cross") replacement = ReplaceMethods("cross", argumentList);
                else if (name == "Distance") replacement = ReplaceMethods("distance", argumentList);
                else if (name == "Dot") replacement = ReplaceMethods("dot", argumentList);
                else if (name == "Lerp") replacement = ReplaceMethods("lerp", argumentList);
                else if (name == "Max") replacement = ReplaceMethods("max", argumentList);
                else if (name == "Min") replacement = ReplaceMethods("min", argumentList);
                else if (name == "Project") replacement = ReplaceMethods("project", argumentList);

                if (replacement != null)
                {
                    ReplacementsCount++;

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }

            return newNode;
        }

        private ExpressionSyntax ReplaceStaticProperties(MemberAccessExpressionSyntax originalNode, float x, float y, float z)
        {
            return SyntaxFactory.ObjectCreationExpression(
                SyntaxFactory.Token(SyntaxKind.NewKeyword).WithTrailingTrivia(SyntaxFactory.Space),
                SyntaxFactory.IdentifierName("float3"),
                SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(new[]
                {
                    SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(x))),
                    SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(y))),
                    SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(z)))
                })).NormalizeWhitespace(),
                null);
        }

        private ExpressionSyntax ReplaceMethods(string method, ArgumentListSyntax argumentList)
        {
            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName("math"),
                    SyntaxFactory.IdentifierName(method)),
                argumentList.NormalizeWhitespace());
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
