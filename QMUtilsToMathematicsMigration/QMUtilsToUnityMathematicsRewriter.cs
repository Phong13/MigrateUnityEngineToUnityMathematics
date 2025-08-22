using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace MigrateToUnityMathematics
{
    class QMUtilsToUnityMathematicsRewriter
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Unity Quaternion Refactor Tool");
            Console.WriteLine("------------------------------------");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: QMUtilsToMathematicsMigration <path_to_unity_project_or_solution_file> [optional: <path_to_config_file>]");
                Console.WriteLine("Example with config file: dotnet run --project QMUtilsToMathematicsMigration.csproj " +
                                  "-- \"C:\\Users\\rowan\\Workspace\\Unity\\Vector3ToFloat3UtilsTesting\\Assembly-CSharp.csproj\" " +
                                  "C:\\Users\\rowan\\Workspace\\Unity\\MigrateUnityEngineToUnityMathematics\\config.txt\"");
                Console.WriteLine("dotnet run --project QMUtilsToMathematicsMigration.csproj " +
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

                    var QMUtilsToUnityMathematicsRewriter = new QMUtilsToUnityMathematicsTransformer(originalSemanticModel);
                    var newSyntaxRoot = QMUtilsToUnityMathematicsRewriter.Visit(originalSyntaxTree.GetRoot());

                    if (newSyntaxRoot != originalSyntaxTree.GetRoot())
                    {
                        totalReplacements += QMUtilsToUnityMathematicsRewriter.ReplacementsCount;

                        newSyntaxRoot = AddRequiredNamespaces((CompilationUnitSyntax)newSyntaxRoot);

                        solution = solution.WithDocumentSyntaxRoot(document.Id, newSyntaxRoot);
                        Console.WriteLine($"    Replaced {QMUtilsToUnityMathematicsRewriter.ReplacementsCount} instances in {Path.GetFileName(document.FilePath)}");
                    }
                }
            }

            Console.WriteLine("\nApplying changes to disk...");
            if (workspace.TryApplyChanges(solution))
            {
                Console.WriteLine($"Successfully applied all changes. Total replacements: {totalReplacements}");
                Console.WriteLine("Remember to add the QuaternionToMathematicsUtils.cs file to your Unity project if you haven't already.");
            }
            else
            {
                Console.WriteLine("Failed to apply changes to disk.");
            }
        }

        private static CompilationUnitSyntax AddRequiredNamespaces(CompilationUnitSyntax root)
        {
            string rewriterNamespace = typeof(QMUtilsToUnityMathematicsRewriter).Namespace;

            var requiredUsings = new List<string> { "Unity.Mathematics" };
            if (!string.IsNullOrEmpty(rewriterNamespace))
                requiredUsings.Add(rewriterNamespace);


            var existingUsings = new HashSet<string>();
            foreach (var u in root.Usings)
            {
                existingUsings.Add(u.Name.ToString());
            }

            var newUsings = new List<UsingDirectiveSyntax>();
            foreach (var u in requiredUsings)
            {
                if (!existingUsings.Contains(u))
                {
                    var usingDirective = SyntaxFactory
                        .UsingDirective(
                            SyntaxFactory.IdentifierName(u)
                                         .WithLeadingTrivia(SyntaxFactory.Space)
                        )
                        .WithTrailingTrivia(SyntaxFactory.ElasticCarriageReturnLineFeed);

                    newUsings.Add(usingDirective);
                }
            }

            return root.AddUsings(newUsings.ToArray());
        }
    }
    /// <summary>
    /// Rewrites simple QuaternionToMathematicsUtils properties, methods, and operators as their underlying logic
    /// </summary>
    class QMUtilsToUnityMathematicsTransformer : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public QMUtilsToUnityMathematicsTransformer(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var originalSymbol = _semanticModel.GetSymbolInfo(node).Symbol;

            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            var originalName = newNode.Name.Identifier.Text;

            if (newNode.Expression is IdentifierNameSyntax id &&
                id.Identifier.Text == "QuaternionToMathematicsUtils" &&
                originalName == "identity")
            {
                ReplacementsCount++;
                return SyntaxFactory.ParseExpression("quaternion.identity")
                                     .WithTriviaFrom(node);
            }

            return newNode;
        }

        //Maybe test Angle_deg, Euler_deg, and definitely Lerp
        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var originalSymbol = _semanticModel.GetSymbolInfo(node.Expression).Symbol as IMethodSymbol;

            var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

            if (originalSymbol?.ContainingType?.Name == "QuaternionToMathematicsUtils")
            {
                if (originalSymbol.Name == "eulerAngles_deg" &&
                    newNode.ArgumentList.Arguments.Count == 1)
                {
                    ReplacementsCount++;
                    return SyntaxFactory.ParseExpression(
                        $"math.degrees(math.Euler(math.normalize({newNode.ArgumentList.Arguments[0]}), math.RotationOrder.ZXY))"
                    ).WithTriviaFrom(newNode);
                }
                if (originalSymbol.Name == "Angle_deg" &&
                    newNode.ArgumentList.Arguments.Count == 2)
                {
                    ReplacementsCount++;
                    return SyntaxFactory.ParseExpression(
                        $"math.degrees(2f * math.acos(math.clamp(math.abs(math.dot(math.normalize({newNode.ArgumentList.Arguments[0]}), math.normalize({newNode.ArgumentList.Arguments[1]}))), -1f, 1f)))"
                    ).WithTriviaFrom(newNode);
                    // I think this works... might be too long?
                    // Had to replace shortest path correction in wrapper: if (dot < 0f) dot = -dot)
                    // with math.abs()
                }
                if (originalSymbol.Name == "AngleAxis_deg" &&
                    newNode.ArgumentList.Arguments.Count == 2)
                {
                    ReplacementsCount++;
                    return SyntaxFactory.ParseExpression(
                        $"quaternion.AxisAngle(math.normalize({newNode.ArgumentList.Arguments[1]}), math.radians({newNode.ArgumentList.Arguments[0]}))"
                    ).WithTriviaFrom(newNode);
                }
                if (originalSymbol.Name == "Euler_deg")
                {
                    ReplacementsCount++;
                    if (newNode.ArgumentList.Arguments.Count == 3)
                    {
                        return SyntaxFactory.ParseExpression(
                            $"quaternion.Euler(math.radians({newNode.ArgumentList.Arguments[0]}), math.radians({newNode.ArgumentList.Arguments[1]}), math.radians({newNode.ArgumentList.Arguments[2]}))"
                        ).WithTriviaFrom(newNode);
                    }
                    else if (newNode.ArgumentList.Arguments.Count == 1)
                    {
                        return SyntaxFactory.ParseExpression(
                            $"quaternion.Euler(math.radians({newNode.ArgumentList.Arguments[0]}.x), math.radians({newNode.ArgumentList.Arguments[0]}.y), math.radians({newNode.ArgumentList.Arguments[0]}.z))"
                        ).WithTriviaFrom(newNode);
                    }
                }
                if (originalSymbol.Name == "Inverse" &&
                    newNode.ArgumentList.Arguments.Count == 1)
                {
                    ReplacementsCount++;
                    return SyntaxFactory.ParseExpression(
                        $"math.inverse({newNode.ArgumentList.Arguments[0]})"
                    ).WithTriviaFrom(newNode);
                }
                // Should work, too complicated?
                /*
                if (originalSymbol.Name == "Lerp" &&
                    newNode.ArgumentList.Arguments.Count == 3)
                {
                    ReplacementsCount++;
                    return SyntaxFactory.ParseExpression(
                        $"math.normalize(new quaternion(math.lerp({newNode.ArgumentList.Arguments[0]}.value, {newNode.ArgumentList.Arguments[1]}.value, math.clamp({newNode.ArgumentList.Arguments[2]}, 0f, 1f))))"
                    ).WithTriviaFrom(newNode);
                }
                */
                if (originalSymbol.Name == "LookRotation")
                {
                    ReplacementsCount++;
                    if (newNode.ArgumentList.Arguments.Count == 1)
                    {
                        return SyntaxFactory.ParseExpression(
                            $"quaternion.LookRotation(math.normalize({newNode.ArgumentList.Arguments[0]}), new float3(0, 1, 0))"
                        ).WithTriviaFrom(newNode);
                    }
                    else if (newNode.ArgumentList.Arguments.Count == 2)
                    {
                        return SyntaxFactory.ParseExpression(
                            $"quaternion.LookRotation(math.normalize({newNode.ArgumentList.Arguments[0]}), math.normalize({newNode.ArgumentList.Arguments[1]}))"
                        ).WithTriviaFrom(newNode);
                    }
                }
                if (originalSymbol.Name == "Normalize" &&
                    newNode.ArgumentList.Arguments.Count == 1 &&
                    newNode.ArgumentList.Arguments[0].RefOrOutKeyword.Kind() != SyntaxKind.RefKeyword)
                {
                    ReplacementsCount++;
                    return SyntaxFactory.ParseExpression(
                        $"math.normalize({newNode.ArgumentList.Arguments[0].Expression})"
                    ).WithTriviaFrom(newNode);
                }
                // Should work, too complicated?
                /*
                if (originalSymbol.Name == "Slerp" &&
                    newNode.ArgumentList.Arguments.Count == 3)
                {
                    ReplacementsCount++;
                    return SyntaxFactory.ParseExpression(
                        $"math.slerp({newNode.ArgumentList.Arguments[0]}, {newNode.ArgumentList.Arguments[1]}, math.clamp({newNode.ArgumentList.Arguments[2]}, 0f, 1f))"
                    ).WithTriviaFrom(newNode);
                }
                */
                // Should work, too complicated?
                /*
                if (originalSymbol.Name == "Equals" &&
                    newNode.ArgumentList.Arguments.Count == 2)
                {
                    ReplacementsCount++;
                    return SyntaxFactory.ParseExpression(
                        $"{newNode.ArgumentList.Arguments[0]}.value.x == {newNode.ArgumentList.Arguments[1]}.value.x && " +
                        $"{newNode.ArgumentList.Arguments[0]}.value.y == {newNode.ArgumentList.Arguments[1]}.value.y && " +
                        $"{newNode.ArgumentList.Arguments[0]}.value.z == {newNode.ArgumentList.Arguments[1]}.value.z && " +
                        $"{newNode.ArgumentList.Arguments[0]}.value.w == {newNode.ArgumentList.Arguments[1]}.value.w"
                    ).WithTriviaFrom(newNode);
                }
                */
                if (originalSymbol.Name == "Multiply" &&
                    newNode.ArgumentList.Arguments.Count == 2)
                {
                    ReplacementsCount++;
                    return SyntaxFactory.ParseExpression(
                        $"math.mul({newNode.ArgumentList.Arguments[0]}, {newNode.ArgumentList.Arguments[1]})"
                    ).WithTriviaFrom(newNode);
                }
            }

            return newNode;
        }

        public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            if (node.Expression is InvocationExpressionSyntax invocation)
            {
                var symbol = _semanticModel.GetSymbolInfo(invocation.Expression).Symbol as IMethodSymbol;

                if (symbol?.ContainingType?.Name == "QuaternionToMathematicsUtils" &&
                    symbol.Name == "Normalize" &&
                    invocation.ArgumentList.Arguments.Count == 1 &&
                    invocation.ArgumentList.Arguments[0].RefOrOutKeyword.Kind() == SyntaxKind.RefKeyword)
                {
                    var arg = invocation.ArgumentList.Arguments[0].Expression;
                    ReplacementsCount++;

                    // Replace "Utils.Normalize(ref q);" with "q = math.normalize(q);"
                    return SyntaxFactory.ParseStatement(
                        $"{arg} = math.normalize({arg});"
                    ).WithTriviaFrom(node);
                }
            }

            return base.VisitExpressionStatement(node);
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
