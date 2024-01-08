using System.Reflection;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;

namespace Odai;

public sealed class Roslyn :
    CodeGenerationBase
{
    public static readonly Roslyn Instance = new();

    protected override GeneratedCode GenerateCode()
    {
        var input = SyntaxFactory.IdentifierName("input");
        var output = SyntaxFactory.IdentifierName("output");
        var culture = SyntaxFactory.IdentifierName("culture");

        var compilationUnit = SyntaxFactory.CompilationUnit()
            .WithUsings(SyntaxFactory.List(
                [
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Globalization")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Odai"))
                ]))
            .WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                SyntaxFactory
                        .FileScopedNamespaceDeclaration(SyntaxFactory.ParseName("Odai"))
                        .WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                            SyntaxFactory
                                .StructDeclaration(SyntaxFactory.Identifier("Roslyn"))
                                .WithModifiers(SyntaxFactory.TokenList(
                                    SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                                    SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword)))
                                .WithBaseList(SyntaxFactory.BaseList(
                                    SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(
                                        SyntaxFactory.SimpleBaseType(
                                            SyntaxFactory.ParseName("IOdai")))))
                                .WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>(
                                    [
                                        SyntaxFactory
                                            .MethodDeclaration(
                                                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                                                SyntaxFactory.Identifier("Invoke"))
                                            .WithModifiers(SyntaxFactory.TokenList(
                                                SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                                            .WithParameterList(SyntaxFactory.ParameterList(
                                                SyntaxFactory.SeparatedList(
                                                    [
                                                        SyntaxFactory
                                                            .Parameter(input.Identifier)
                                                            .WithType(SyntaxFactory.ParseTypeName("ReadOnlySpan<char>")),
                                                        SyntaxFactory
                                                            .Parameter(output.Identifier)
                                                            .WithType(SyntaxFactory.ParseTypeName("Span<char>"))
                                                    ])))
                                            .WithBody(SyntaxFactory.Block(
                                                SyntaxFactory.LocalDeclarationStatement(
                                                    SyntaxFactory.VariableDeclaration(
                                                        SyntaxFactory.IdentifierName(SyntaxFactory.Identifier(default, SyntaxKind.VarKeyword, "var", "var", default)),
                                                        SyntaxFactory.SingletonSeparatedList(
                                                            SyntaxFactory
                                                                .VariableDeclarator(culture.Identifier)
                                                                .WithInitializer(
                                                                    SyntaxFactory.EqualsValueClause(
                                                                        SyntaxFactory.MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            SyntaxFactory.ParseTypeName("CultureInfo"),
                                                                            SyntaxFactory.IdentifierName("InvariantCulture"))))))),
                                                SyntaxFactory.ExpressionStatement(
                                                    SyntaxFactory.InvocationExpression(
                                                        SyntaxFactory.MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            SyntaxFactory.InvocationExpression(
                                                                SyntaxFactory.MemberAccessExpression(
                                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                                    SyntaxFactory.ParseName("DateTime"),
                                                                    SyntaxFactory.IdentifierName("ParseExact")),
                                                                SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(
                                                                    [
                                                                        SyntaxFactory.Argument(input),
                                                                        SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("MM-dd-yyyy"))),
                                                                        SyntaxFactory.Argument(culture)
                                                                    ]))),
                                                            SyntaxFactory.IdentifierName("TryFormat")),
                                                        SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(
                                                            [
                                                                SyntaxFactory.Argument(output),
                                                                SyntaxFactory.Argument(default, SyntaxFactory.Token(SyntaxKind.OutKeyword), SyntaxFactory.IdentifierName(SyntaxFactory.Identifier(default, SyntaxKind.UnderscoreToken, "_", "_", default))),
                                                                SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("yyyy/MM/dd"))),
                                                                SyntaxFactory.Argument(culture)
                                                            ]))))))
                                    ]))))));

        var syntaxTree = CSharpSyntaxTree.Create(
            compilationUnit,
            new CSharpParseOptions(
                LanguageVersion.CSharp12,
                DocumentationMode.None));

        var compilation = CSharpCompilation.Create(
            null,
            [syntaxTree],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location), 
                MetadataReference.CreateFromFile(typeof(IOdai).Assembly.Location)
            ],
            new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Release,
                concurrentBuild: false));

        using var peStream = new MemoryStream();
        using var pdbStream = new MemoryStream();

        var emitResult = compilation.Emit(
            peStream,
            pdbStream,
            options: new EmitOptions(
                debugInformationFormat: DebugInformationFormat.PortablePdb));

        var assembly = Assembly.Load(peStream.GetBuffer(), pdbStream.GetBuffer());

        var type = assembly.GetType("Odai.Roslyn");
        var instance = (IOdai)Activator.CreateInstance(type);

        return instance.Invoke;
    }
}
