using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace MigrateToUnityMathematics
{
    class QuaternionToQMUtilsRewriter
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Unity Quaternion Refactor Tool");
            Console.WriteLine("------------------------------------");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: QuaternionToQMUtilsMigration <path_to_unity_project_or_solution_file> [optional: <path_to_config_file>]");
                Console.WriteLine("Example with config file: dotnet run --project QuaternionToQMUtilsMigration.csproj " +
                                  "-- \"C:\\Users\\rowan\\Workspace\\Unity\\Vector3ToFloat3UtilsTesting\\Assembly-CSharp.csproj\" " +
                                  "C:\\Users\\rowan\\Workspace\\Unity\\MigrateUnityEngineToUnityMathematics\\config.txt\"");
                Console.WriteLine("dotnet run --project QuaternionToQMUtilsMigration.csproj " +
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

                    var QuaternionToQMUtilsRewriter = new QuaternionToQMUtilsTransformer(originalSemanticModel);
                    var newSyntaxRoot = QuaternionToQMUtilsRewriter.Visit(originalSyntaxTree.GetRoot());

                    if (newSyntaxRoot != originalSyntaxTree.GetRoot())
                    {
                        totalReplacements += QuaternionToQMUtilsRewriter.ReplacementsCount;
                        solution = solution.WithDocumentSyntaxRoot(document.Id, newSyntaxRoot);
                        Console.WriteLine($"    Replaced {QuaternionToQMUtilsRewriter.ReplacementsCount} instances in {Path.GetFileName(document.FilePath)}");
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
    }

    class QuaternionToQMUtilsTransformer : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public QuaternionToQMUtilsTransformer(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            var originalLeftType = _semanticModel.GetTypeInfo(node.Left).Type;
            var originalRightType = _semanticModel.GetTypeInfo(node.Right).Type;
            var originalKind = node.Kind();

            var newNode = (BinaryExpressionSyntax)base.VisitBinaryExpression(node);

            if (originalKind == SyntaxKind.EqualsExpression || originalKind == SyntaxKind.MultiplyExpression)
            {
                if (originalLeftType?.ToDisplayString() == "UnityEngine.Quaternion" &&
                    originalRightType?.ToDisplayString() == "UnityEngine.Quaternion")
                {
                    ReplacementsCount++;

                    string newNodeName;
                    if (originalKind == SyntaxKind.EqualsExpression)
                    {
                        newNodeName = "Equals";
                    }
                    else
                    {
                        newNodeName = "Multiply";
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
                        SyntaxFactory.IdentifierName("QuaternionToMathematicsUtils"),
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

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var originalSymbol = _semanticModel.GetSymbolInfo(node).Symbol;

            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            string memberName = newNode.Name.Identifier.Text;

            if (originalSymbol is IPropertySymbol propSymbol &&
                propSymbol.ContainingType != null &&
                propSymbol.ContainingType.ToDisplayString() == "UnityEngine.Quaternion" &&
                propSymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
            {
                if (memberName == "eulerAngles" || memberName == "normalized")
                {
                    ReplacementsCount++;

                    var expression = newNode.Expression;

                    var argumentList = SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Argument(expression.WithoutTrivia())
                        )
                    );

                    string newName = memberName;
                    if (newName == "eulerAngles") newName = "eulerAngles_deg";

                    var memberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("QuaternionToMathematicsUtils"),
                        SyntaxFactory.IdentifierName(newName)
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        memberAccess,
                        argumentList.NormalizeWhitespace()
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
                if (memberName == "identity")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("QuaternionToMathematicsUtils"),
                        SyntaxFactory.IdentifierName(memberName)
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            else if (originalSymbol is IFieldSymbol fieldSymbol &&
                     fieldSymbol.ContainingType?.ToDisplayString() == "UnityEngine.Quaternion")
            {
                if (memberName == "x" || memberName == "y" || memberName == "z" || memberName == "w")
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
                        SyntaxFactory.IdentifierName("QuaternionToMathematicsUtils"),
                        SyntaxFactory.IdentifierName(memberName)
                    );

                    return SyntaxFactory.InvocationExpression(memberAccess, argumentList)
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }

            return newNode;
        }

        public override SyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            var newNode = (AssignmentExpressionSyntax)base.VisitAssignmentExpression(node);

            if (newNode.Left is ElementAccessExpressionSyntax elementAccess)
            {
                var targetType = _semanticModel.GetTypeInfo(elementAccess.Expression).Type;
                string typeName = targetType.ToDisplayString();

                if (typeName == "UnityEngine.Quaternion")
                {
                    var elementExpr = elementAccess.Expression.WithoutTrailingTrivia();
                    var indexArg = elementAccess.ArgumentList.Arguments[0];
                    var valueExpr = newNode.Right;

                    var args = SyntaxFactory.ArgumentList(
                        SyntaxFactory.SeparatedList(new[]
                        {
                            SyntaxFactory.Argument(elementExpr).WithRefOrOutKeyword(SyntaxFactory.Token(SyntaxKind.RefKeyword)),
                            indexArg,
                            SyntaxFactory.Argument(valueExpr)
                        })
                    );

                    var memberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("QuaternionToMathematicsUtils"),
                        SyntaxFactory.IdentifierName("SetIndex")
                    );

                    var invocation = SyntaxFactory.InvocationExpression(memberAccess, args.NormalizeWhitespace())
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());

                    return invocation;
                }
            }

            return newNode;
        }

        public override SyntaxNode VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            var targetType = _semanticModel.GetTypeInfo(node.Expression).Type;
            string typeName = targetType?.ToDisplayString();

            var newNode = (ElementAccessExpressionSyntax)base.VisitElementAccessExpression(node);

            if (typeName == "UnityEngine.Quaternion" &&
                !(node.Parent is AssignmentExpressionSyntax && ((AssignmentExpressionSyntax)node.Parent).Left == node))
            // v = this[0] is fine, but this[0] = v is not
            {
                var elementExpr = newNode.Expression;
                var indexArg = newNode.ArgumentList.Arguments[0];

                var args = SyntaxFactory.ArgumentList(
                    SyntaxFactory.SeparatedList(new[]
                    {
                        SyntaxFactory.Argument(elementExpr),
                        indexArg
                    })
                );

                var memberAccess = SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName("QuaternionToMathematicsUtils"),
                    SyntaxFactory.IdentifierName("GetIndex")
                );

                var invocation = SyntaxFactory.InvocationExpression(memberAccess, args.NormalizeWhitespace())
                    .WithLeadingTrivia(newNode.GetLeadingTrivia())
                    .WithTrailingTrivia(newNode.GetTrailingTrivia());

                return invocation;
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
                    string newName = memberAccessName;

                    if (originalSymbol is IMethodSymbol methodSymbol &&
                        methodSymbol.ContainingType != null &&
                        methodSymbol.ContainingType.ToDisplayString() == "UnityEngine.Quaternion")
                    {
                        if (methodSymbol.Parameters.Length == 2 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Quaternion" &&
                            methodSymbol.Parameters[1].Type.ToDisplayString() == "UnityEngine.Quaternion" &&
                            (memberAccessName == "Angle" || memberAccessName == "Dot"))
                        {
                            if (newName == "Angle") newName = "Angle_deg";

                            return CreateReplacementInvocation(newName, newNode.ArgumentList, newNode);
                        }

                        if (methodSymbol.Parameters.Length == 2 &&
                            methodSymbol.Parameters[0].Type.SpecialType == SpecialType.System_Single &&
                            methodSymbol.Parameters[1].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            (memberAccessName == "AngleAxis"))
                        {
                            if (newName == "AngleAxis") newName = "AngleAxis_deg";

                            return CreateReplacementInvocation(newName, newNode.ArgumentList, newNode);
                        }

                        if (methodSymbol.Parameters.Length == 1 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            (memberAccessName == "Euler" || memberAccessName == "LookRotation"))
                        {
                            if (newName == "Euler") newName = "Euler_deg";

                            return CreateReplacementInvocation(newName, newNode.ArgumentList, newNode);
                        }

                        if (methodSymbol.Parameters.Length == 2 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            methodSymbol.Parameters[1].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            (memberAccessName == "FromToRotation" || memberAccessName == "LookRotation"))
                        {
                            return CreateReplacementInvocation(newName, newNode.ArgumentList, newNode);
                        }

                        if (methodSymbol.Parameters.Length == 1 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Quaternion" &&
                            methodSymbol.IsStatic &&
                            (memberAccessName == "Inverse" || memberAccessName == "Normalize"))
                        {
                            return CreateReplacementInvocation(newName, newNode.ArgumentList, newNode);
                        }
                        if (methodSymbol.Parameters.Length == 0 &&
                            methodSymbol.ContainingType.ToString() == "UnityEngine.Quaternion" &&
                            !methodSymbol.IsStatic &&
                            memberAccessName == "Normalize")
                        {
                            var instanceExpr = ((MemberAccessExpressionSyntax)newNode.Expression).Expression;

                            var refArg = SyntaxFactory.Argument(instanceExpr)
                                .WithRefKindKeyword(SyntaxFactory.Token(SyntaxKind.RefKeyword));

                            var args = SyntaxFactory.SingletonSeparatedList(refArg);

                            return CreateReplacementInvocation("Normalize",
                                SyntaxFactory.ArgumentList(args), newNode);
                        }

                        if (methodSymbol.Parameters.Length == 3 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Quaternion" &&
                            methodSymbol.Parameters[1].Type.ToDisplayString() == "UnityEngine.Quaternion" &&
                            methodSymbol.Parameters[2].Type.SpecialType == SpecialType.System_Single &&
                            (memberAccessName == "Lerp" || memberAccessName == "LerpUnclamped" ||
                            memberAccessName == "Slerp" || memberAccessName == "SlerpUnclamped" ||
                            memberAccessName == "RotateTowards"))
                        {
                            return CreateReplacementInvocation(newName, newNode.ArgumentList, newNode);
                        }

                        if (methodSymbol.Parameters.Length == 3 &&
                            methodSymbol.Parameters[0].Type.SpecialType == SpecialType.System_Single &&
                            methodSymbol.Parameters[1].Type.SpecialType == SpecialType.System_Single &&
                            methodSymbol.Parameters[2].Type.SpecialType == SpecialType.System_Single &&
                            (memberAccessName == "Euler"))
                        {
                            newName = "Euler_deg";

                            return CreateReplacementInvocation(newName, newNode.ArgumentList, newNode);
                        }
                        if (methodSymbol.Parameters.Length == 4 &&
                            methodSymbol.Parameters[0].Type.SpecialType == SpecialType.System_Single &&
                            methodSymbol.Parameters[1].Type.SpecialType == SpecialType.System_Single &&
                            methodSymbol.Parameters[2].Type.SpecialType == SpecialType.System_Single &&
                            methodSymbol.Parameters[3].Type.SpecialType == SpecialType.System_Single &&
                            memberAccessName == "Set")
                        {
                            var instanceExpr = memberAccess.Expression;
                            var refArg = SyntaxFactory.Argument(instanceExpr)
                                .WithRefKindKeyword(SyntaxFactory.Token(SyntaxKind.RefKeyword));

                            var args = newNode.ArgumentList.Arguments.Insert(0, refArg);

                            return CreateReplacementInvocation(newName,
                                SyntaxFactory.ArgumentList(args), newNode);
                        }

                        if (methodSymbol.Parameters.Length == 2 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            methodSymbol.Parameters[1].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            (memberAccessName == "SetFromToRotation" || memberAccessName == "SetLookRotation"))
                        {
                            var instanceExpr = memberAccess.Expression;
                            var refArg = SyntaxFactory.Argument(instanceExpr)
                                .WithRefKindKeyword(SyntaxFactory.Token(SyntaxKind.RefKeyword));

                            var args = newNode.ArgumentList.Arguments.Insert(0, refArg);

                            return CreateReplacementInvocation(newName,
                                SyntaxFactory.ArgumentList(args), newNode);
                        }

                        if (methodSymbol.Parameters.Length == 1 &&
                            methodSymbol.Parameters[0].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            memberAccessName == "SetLookRotation")
                        {
                            var instanceExpr = memberAccess.Expression;
                            var refArg = SyntaxFactory.Argument(instanceExpr)
                                .WithRefKindKeyword(SyntaxFactory.Token(SyntaxKind.RefKeyword));

                            var args = newNode.ArgumentList.Arguments.Insert(0, refArg);

                            return CreateReplacementInvocation(newName,
                                SyntaxFactory.ArgumentList(args), newNode);
                        }

                        if (methodSymbol.Parameters.Length == 2 &&
                            methodSymbol.Parameters[0].RefKind == RefKind.Out &&
                            methodSymbol.Parameters[0].Type.SpecialType == SpecialType.System_Single &&
                            methodSymbol.Parameters[1].RefKind == RefKind.Out &&
                            methodSymbol.Parameters[1].Type.ToDisplayString() == "UnityEngine.Vector3" &&
                            memberAccessName == "ToAngleAxis")
                        {
                            newName = "ToAngleAxis_deg";
                            var instanceExpr = memberAccess.Expression;
                            var refArg = SyntaxFactory.Argument(instanceExpr)
                                .WithRefKindKeyword(SyntaxFactory.Token(SyntaxKind.RefKeyword));

                            var args = newNode.ArgumentList.Arguments.Insert(0, refArg);

                            return CreateReplacementInvocation(newName,
                                SyntaxFactory.ArgumentList(args), newNode);
                        }
                        if (memberAccessName == "ToString" &&
                            methodSymbol.Parameters.Length == 0 &&
                            methodSymbol.ContainingType.ToString() == "UnityEngine.Quaternion" &&
                            !methodSymbol.IsStatic)
                        {
                            var instanceExpr = memberAccess.Expression;

                            var args = SyntaxFactory.SeparatedList(new[]
                            {
                                SyntaxFactory.Argument(instanceExpr)
                            });

                            return CreateReplacementInvocation("ToString",
                                SyntaxFactory.ArgumentList(args), newNode);
                        }
                    }

                    return newNode;
                }
            }

            return base.VisitInvocationExpression(node);
        }
        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var symbol = _semanticModel.GetSymbolInfo(node).Symbol as IMethodSymbol;

            if (symbol == null) return base.VisitObjectCreationExpression(node);

            if (symbol.ContainingType.ToString() == "UnityEngine.Quaternion" &&
                symbol.Parameters.Length == 4 &&
                symbol.Parameters.All(p => p.Type.SpecialType == SpecialType.System_Single))
            {
                return CreateReplacementInvocation(
                    "Quaternion",
                    node.ArgumentList,
                    node
                );
            }

            return base.VisitObjectCreationExpression(node);
        }

        private InvocationExpressionSyntax CreateReplacementInvocation(string newName, ArgumentListSyntax arguments, SyntaxNode originalNode)
        {
            ReplacementsCount++;

            var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.IdentifierName("QuaternionToMathematicsUtils"),
                SyntaxFactory.IdentifierName(newName)
            );

            var invocationExpression = SyntaxFactory.InvocationExpression(
                newMemberAccess,
                arguments.NormalizeWhitespace()
            );

            return invocationExpression
                .WithLeadingTrivia(originalNode.GetLeadingTrivia())
                .WithTrailingTrivia(originalNode.GetTrailingTrivia()); ;
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


