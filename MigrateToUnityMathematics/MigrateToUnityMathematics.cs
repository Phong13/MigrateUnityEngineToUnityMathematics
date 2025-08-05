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

                        var magnitudePropertyRewriter = new MagnitudePropertyRewriter(originalSemanticModel);
                        var rootAfterMagnitudePropertyRewriter = magnitudePropertyRewriter.Visit(originalSyntaxTree.GetRoot());

                        var sqrMagnitudePropertyRewriter = new SqrMagnitudePropertyRewriter(originalSemanticModel);
                        var rootAfterSqrMagnitudePropertyRewriter = sqrMagnitudePropertyRewriter.Visit(rootAfterMagnitudePropertyRewriter);

                        var normalizedPropertyRewriter = new NormalizedPropertyRewriter(originalSemanticModel);
                        var rootAfterNormalizedPropertyRewriter = normalizedPropertyRewriter.Visit(rootAfterSqrMagnitudePropertyRewriter);

                        var forwardPropertyRewriter = new ForwardPropertyRewriter(originalSemanticModel);
                        var rootAfterForwardPropertyRewriter = forwardPropertyRewriter.Visit(rootAfterNormalizedPropertyRewriter);

                        var backPropertyRewriter = new BackPropertyRewriter(originalSemanticModel);
                        var rootAfterBackPropertyRewriter = backPropertyRewriter.Visit(rootAfterForwardPropertyRewriter);

                        var upPropertyRewriter = new UpPropertyRewriter(originalSemanticModel);
                        var rootAfterUpPropertyRewriter = upPropertyRewriter.Visit(rootAfterBackPropertyRewriter);

                        var downPropertyRewriter = new DownPropertyRewriter(originalSemanticModel);
                        var rootAfterDownPropertyRewriter = downPropertyRewriter.Visit(rootAfterUpPropertyRewriter);

                        var rightPropertyRewriter = new RightPropertyRewriter(originalSemanticModel);
                        var rootAfterRightPropertyRewriter = rightPropertyRewriter.Visit(rootAfterDownPropertyRewriter);

                        var leftPropertyRewriter = new LeftPropertyRewriter(originalSemanticModel);
                        var rootAfterLeftPropertyRewriter = leftPropertyRewriter.Visit(rootAfterRightPropertyRewriter);

                        var onePropertyRewriter = new OnePropertyRewriter(originalSemanticModel);
                        var rootAfterOnePropertyRewriter = onePropertyRewriter.Visit(rootAfterLeftPropertyRewriter);

                        var zeroPropertyRewriter = new ZeroPropertyRewriter(originalSemanticModel);
                        var rootAfterZeroPropertyRewriter = zeroPropertyRewriter.Visit(rootAfterOnePropertyRewriter);

                        var equalsOperatorRewriter = new EqualsOperatorRewriter(originalSemanticModel);
                        var rootAfterEqualsOperatorRewriter = equalsOperatorRewriter.Visit(rootAfterZeroPropertyRewriter);

                        var notEqualsOperatorRewriter = new NotEqualsOperatorRewriter(originalSemanticModel);
                        var rootAfterNotEqualsOperator = notEqualsOperatorRewriter.Visit(rootAfterEqualsOperatorRewriter);

                        var angle_degMethodRewriter = new Angle_degMethodRewriter(originalSemanticModel);
                        var rootAfterAngle_degMethodRewriter = angle_degMethodRewriter.Visit(rootAfterNotEqualsOperator);

                        var crossMethodRewriter = new CrossMethodRewriter(originalSemanticModel);
                        var rootAfterCrossMethodRewriter = crossMethodRewriter.Visit(rootAfterAngle_degMethodRewriter);

                        var distanceMethodRewriter = new DistanceMethodRewriter(originalSemanticModel);
                        var rootAfterDistanceMethodRewriter = distanceMethodRewriter.Visit(rootAfterCrossMethodRewriter);

                        var dotMethodRewriter = new DotMethodRewriter(originalSemanticModel);
                        var rootAfterDotMethodRewriter = dotMethodRewriter.Visit(rootAfterDistanceMethodRewriter);

                        var lerpMethodRewriter = new LerpMethodRewriter(originalSemanticModel);
                        var rootAfterLerpMethodRewriter = lerpMethodRewriter.Visit(rootAfterDotMethodRewriter);

                        var maxMethodRewriter = new MaxMethodRewriter(originalSemanticModel);
                        var rootAfterMaxMethodRewriter = maxMethodRewriter.Visit(rootAfterLerpMethodRewriter);

                        var minMethodRewriter = new MinMethodRewriter(originalSemanticModel);
                        var rootAfterMinMethodRewriter = minMethodRewriter.Visit(rootAfterMaxMethodRewriter);

                        var moveTowardsMethodRewriter = new MoveTowardsMethodRewriter(originalSemanticModel);
                        var rootAfterTowardsMethodRewriter = moveTowardsMethodRewriter.Visit(rootAfterMinMethodRewriter);

                        var normalizeMethodRewriter = new NormalizeMethodRewriter(originalSemanticModel);
                        var rootAfterNormalizeMethodRewriter = normalizeMethodRewriter.Visit(rootAfterTowardsMethodRewriter);

                        var projectMethodRewriter = new ProjectMethodRewriter(originalSemanticModel);
                        var rootAfterProjectMethodRewriter = projectMethodRewriter.Visit(rootAfterNormalizeMethodRewriter);

                        var projectOnPlaneMethodRewriter = new ProjectOnPlaneMethodRewriter(originalSemanticModel);
                        var newSyntaxRoot = projectOnPlaneMethodRewriter.Visit(rootAfterProjectMethodRewriter);

                        if (newSyntaxRoot != originalSyntaxTree.GetRoot())
                        {
                            totalReplacements += (
                                magnitudePropertyRewriter.ReplacementsCount
                                + sqrMagnitudePropertyRewriter.ReplacementsCount
                                + normalizedPropertyRewriter.ReplacementsCount
                                + forwardPropertyRewriter.ReplacementsCount
                                + backPropertyRewriter.ReplacementsCount
                                + upPropertyRewriter.ReplacementsCount
                                + downPropertyRewriter.ReplacementsCount
                                + rightPropertyRewriter.ReplacementsCount
                                + leftPropertyRewriter.ReplacementsCount
                                + onePropertyRewriter.ReplacementsCount
                                + zeroPropertyRewriter.ReplacementsCount
                                + equalsOperatorRewriter.ReplacementsCount
                                + notEqualsOperatorRewriter.ReplacementsCount
                                + angle_degMethodRewriter.ReplacementsCount
                                + crossMethodRewriter.ReplacementsCount
                                + distanceMethodRewriter.ReplacementsCount
                                + dotMethodRewriter.ReplacementsCount
                                + lerpMethodRewriter.ReplacementsCount
                                + maxMethodRewriter.ReplacementsCount
                                + minMethodRewriter.ReplacementsCount
                                + moveTowardsMethodRewriter.ReplacementsCount
                                + normalizeMethodRewriter.ReplacementsCount
                                + projectMethodRewriter.ReplacementsCount
                                + projectOnPlaneMethodRewriter.ReplacementsCount
                                );
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
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.magnitude' property access
    /// with 'Vector3ToFloat3Utils.magnitude(vector)'.
    /// </summary>
    class MagnitudePropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public MagnitudePropertyRewriter(SemanticModel semanticModel)
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
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"), // The class name
                        SyntaxFactory.IdentifierName("magnitude")       // The static method name
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

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.sqrMagnitude' property access
    /// with 'Vector3ToFloat3Utils.sqrMagnitude(vector)'.
    /// </summary>
    class SqrMagnitudePropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public SqrMagnitudePropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "sqrMagnitude")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
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
                        SyntaxFactory.IdentifierName("sqrMagnitude") 
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
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.normalized' property access
    /// with 'Vector3ToFloat3Utils.normalized(vector)'.
    /// </summary>
    class NormalizedPropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public NormalizedPropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "normalized")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
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
                        SyntaxFactory.IdentifierName("normalized")
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
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.forward' property access
    /// with 'Vector3ToFloat3Utils.forward'.
    /// </summary>
    class ForwardPropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public ForwardPropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "forward")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("forward")
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.back' property access
    /// with 'Vector3ToFloat3Utils.back'.
    /// </summary>
    class BackPropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public BackPropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "back")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("back")
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.up' property access
    /// with 'Vector3ToFloat3Utils.up'.
    /// </summary>
    class UpPropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public UpPropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "up")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("up")
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.down' property access
    /// with 'Vector3ToFloat3Utils.down'.
    /// </summary>
    class DownPropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public DownPropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "down")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("down")
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.right' property access
    /// with 'Vector3ToFloat3Utils.right'.
    /// </summary>
    class RightPropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public RightPropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "right")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("right")
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.left' property access
    /// with 'Vector3ToFloat3Utils.left'.
    /// </summary>
    class LeftPropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public LeftPropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "left")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("left")
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.one' property access
    /// with 'Vector3ToFloat3Utils.one'.
    /// </summary>
    class OnePropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public OnePropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "one")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("one")
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.zero' property access
    /// with 'Vector3ToFloat3Utils.zero'.
    /// </summary>
    class ZeroPropertyRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public ZeroPropertyRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);

            if (newNode.Name.Identifier.Text == "zero")
            {
                var symbol = _semanticModel.GetSymbolInfo(newNode).Symbol;

                if (symbol is IPropertySymbol propertySymbol &&
                    propertySymbol.ContainingType.Name == "Vector3" &&
                    propertySymbol.ContainingType.ContainingNamespace?.ToDisplayString() == "UnityEngine")
                {
                    ReplacementsCount++;

                    var replacement = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("zero")
                    );

                    return replacement
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3 == Vector3' operator access
    /// with 'Vector3ToFloat3Utils.equals(vector1, vector2)'.
    /// </summary>
    class EqualsOperatorRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public EqualsOperatorRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            var newNode = (BinaryExpressionSyntax)base.VisitBinaryExpression(node);

            if (newNode.Kind() == SyntaxKind.EqualsExpression)
            {
                var leftType = _semanticModel.GetTypeInfo(newNode.Left).Type;
                var rightType = _semanticModel.GetTypeInfo(newNode.Right).Type;

                if (leftType?.ToDisplayString() == "UnityEngine.Vector3" &&
                    rightType?.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

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
                        SyntaxFactory.IdentifierName("equals")
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
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3 != Vector3' operator access
    /// with 'Vector3ToFloat3Utils.equals(vector1, vector2)'.
    /// </summary>
    class NotEqualsOperatorRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public NotEqualsOperatorRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            var newNode = (BinaryExpressionSyntax)base.VisitBinaryExpression(node);

            if (newNode.Kind() == SyntaxKind.NotEqualsExpression)
            {
                var leftType = _semanticModel.GetTypeInfo(newNode.Left).Type;
                var rightType = _semanticModel.GetTypeInfo(newNode.Right).Type;

                if (leftType?.ToDisplayString() == "UnityEngine.Vector3" &&
                    rightType?.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

                    var argumentList = SyntaxFactory.ArgumentList(
                        SyntaxFactory.SeparatedList(new[]
                        {
                        SyntaxFactory.Argument(node.Left),
                        SyntaxFactory.Argument(node.Right)
                        })
                    );

                    var memberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("notEquals")
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
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.Angle(vector1, vector2)' method access
    /// with 'Vector3ToFloat3Utils.Angle_deg(vector1, vector2)'.
    /// </summary>
    class Angle_degMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public Angle_degMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

            if (newNode.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "Angle")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.Parameters.Length == 2 &&
                methodSymbol.Parameters[0].ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                methodSymbol.Parameters[1].ContainingType.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("Angle_deg")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        newNode.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.Cross(vector1, vector2)' method access
    /// with 'Vector3ToFloat3Utils.Cross(vector1, vector2)'.
    /// </summary>
    class CrossMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public CrossMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

            if (newNode.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "Cross")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.Parameters.Length == 2 &&
                methodSymbol.Parameters[0].ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                methodSymbol.Parameters[1].ContainingType.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("Cross")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        newNode.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.Distance(vector1, vector2)' method access
    /// with 'Vector3ToFloat3Utils.Distance(vector1, vector2)'.
    /// </summary>
    class DistanceMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public DistanceMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

            if (newNode.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "Distance")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.Parameters.Length == 2 &&
                methodSymbol.Parameters[0].ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                methodSymbol.Parameters[1].ContainingType.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("Distance")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        newNode.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.Dot(vector1, vector2)' method access
    /// with 'Vector3ToFloat3Utils.Dot(vector1, vector2)'.
    /// </summary>
    class DotMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public DotMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

            if (newNode.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "Dot")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.Parameters.Length == 2 &&
                methodSymbol.Parameters[0].ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                methodSymbol.Parameters[1].ContainingType.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("Dot")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        newNode.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.Lerp(vector1, vector2, float)' method access
    /// with 'Vector3ToFloat3Utils.Lerp(vector1, vector2, float)'.
    /// </summary>
    class LerpMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public LerpMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

            if (newNode.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "Lerp")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.Parameters.Length == 3 &&
                methodSymbol.Parameters[0].ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                methodSymbol.Parameters[1].ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                methodSymbol.Parameters[1].ContainingType.ToDisplayString() == "UnityEngine.float3")
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("Lerp")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        newNode.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.Max(vector1, vector2)' method access
    /// with 'Vector3ToFloat3Utils.Max(vector1, vector2)'.
    /// </summary>
    class MaxMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public MaxMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

            if (newNode.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "Max")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.Parameters.Length == 2 &&
                methodSymbol.Parameters[0].ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                methodSymbol.Parameters[1].ContainingType.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("Max")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        newNode.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.Min(vector1, vector2)' method access
    /// with 'Vector3ToFloat3Utils.Min(vector1, vector2)'.
    /// </summary>
    class MinMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public MinMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var newNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);

            if (newNode.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "Min")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.Parameters.Length == 2 &&
                methodSymbol.Parameters[0].ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                methodSymbol.Parameters[1].ContainingType.ToDisplayString() == "UnityEngine.Vector3")
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("Min")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        newNode.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(newNode.GetLeadingTrivia())
                        .WithTrailingTrivia(newNode.GetTrailingTrivia());
                }
            }
            return newNode;
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.MoveTowards(vector1, vector2, float)' method access
    /// with 'Vector3ToFloat3Utils.MoveTowards(vector1, vector2, float)'.
    /// </summary>
    class MoveTowardsMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public MoveTowardsMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (node.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "MoveTowards")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.ContainingType.ToDisplayString() == "UnityEngine.Vector3"
                && methodSymbol.Parameters.Length == 3)
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("MoveTowards")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        node.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(node.GetLeadingTrivia())
                        .WithTrailingTrivia(node.GetTrailingTrivia());
                }
            }
            return base.VisitInvocationExpression(node);
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.Normalize(ref Vector3)' method access
    /// with 'Vector3ToFloat3Utils.Normalize(ref Vector3)'.
    /// </summary>
    class NormalizeMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public NormalizeMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (node.Expression is MemberAccessExpressionSyntax memberAccess &&
                memberAccess.Name.Identifier.Text == "Normalize")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                    methodSymbol.ContainingType.ToDisplayString() == "UnityEngine.Vector3" &&
                    methodSymbol.Parameters.Length == 1 &&
                    methodSymbol.Parameters[0].RefKind == RefKind.Ref)
                {
                    ReplacementsCount++;

                    var originalArg = node.ArgumentList.Arguments[0];

                    var refArgument = SyntaxFactory.Argument(originalArg.Expression)
                        .WithRefKindKeyword(SyntaxFactory.Token(SyntaxKind.RefKeyword));

                    var argumentList = SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList(refArgument)
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
                        .WithLeadingTrivia(node.GetLeadingTrivia())
                        .WithTrailingTrivia(node.GetTrailingTrivia());
                }
            }

            return base.VisitInvocationExpression(node);
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.Project(vector1, vector2)' method access
    /// with 'Vector3ToFloat3Utils.Project(vector1, vector2)'.
    /// </summary>
    class ProjectMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public ProjectMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (node.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "Project")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.ContainingType.ToDisplayString() == "UnityEngine.Vector3"
                && methodSymbol.Parameters.Length == 2)
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("Project")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        node.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(node.GetLeadingTrivia())
                        .WithTrailingTrivia(node.GetTrailingTrivia());
                }
            }
            return base.VisitInvocationExpression(node);
        }
    }

    /// <summary>
    /// This SyntaxRewriter visits the syntax tree and replaces 'Vector3.ProjectOnPlane(vector1, vector2)' method access
    /// with 'Vector3ToFloat3Utils.ProjectOnPlane(vector1, vector2)'.
    /// </summary>
    class ProjectOnPlaneMethodRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public int ReplacementsCount { get; private set; } = 0;

        public ProjectOnPlaneMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (node.Expression is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.Text == "ProjectOnPlane")
            {
                var symbol = _semanticModel.GetSymbolInfo(memberAccess).Symbol;

                if (symbol is IMethodSymbol methodSymbol &&
                methodSymbol.ContainingType.ToDisplayString() == "UnityEngine.Vector3"
                && methodSymbol.Parameters.Length == 2)
                {
                    ReplacementsCount++;

                    var newMemberAccess = SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Vector3ToFloat3Utils"),
                        SyntaxFactory.IdentifierName("ProjectOnPlane")
                    );

                    var invocationExpression = SyntaxFactory.InvocationExpression(
                        newMemberAccess,
                        node.ArgumentList
                    );

                    return invocationExpression
                        .WithLeadingTrivia(node.GetLeadingTrivia())
                        .WithTrailingTrivia(node.GetTrailingTrivia());
                }
            }
            return base.VisitInvocationExpression(node);
        }
    }
}