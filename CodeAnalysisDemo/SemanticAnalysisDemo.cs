using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysisDemo
{
    public class SemanticAnalysisDemo
    {
        public void Start()
        {
            var code = @"using System;
using System.Collections.Generic;
using System.Text;
 
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}";
            BindName(code);

            BindExpression(code);
        }
        /// <summary>
        /// 绑定名称
        /// </summary>
        /// <param name="code"></param>
        public void BindName(string code)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

            var root = (CompilationUnitSyntax)tree.GetRoot();

            var compilation = CSharpCompilation.Create("HelloWorld")
                                              .AddReferences(
                                                   MetadataReference.CreateFromFile(
                                                       typeof(object).Assembly.Location))
                                              .AddSyntaxTrees(tree);

            var model = compilation.GetSemanticModel(tree);

            var nameInfo = model.GetSymbolInfo(root.Usings[0].Name);
            //可获得具体得命名空间
            var systemSymbol = (INamespaceSymbol)nameInfo.Symbol;

            Console.WriteLine("systemSymbol.GetNamespaceMembers-------------------------------------------");
            foreach (var ns in systemSymbol.GetNamespaceMembers())
            {
                Console.WriteLine(ns.Name);
            }
        }
        /// <summary>
        /// 绑定表达式
        /// </summary>
        /// <param name="code"></param>
        public void BindExpression(string code)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var helloWorldString = root.DescendantNodes()
                                      .OfType<LiteralExpressionSyntax>()
                                      .First();
            var compilation = CSharpCompilation.Create("HelloWorld")
                                            .AddReferences(
                                                 MetadataReference.CreateFromFile(
                                                     typeof(object).Assembly.Location))
                                            .AddSyntaxTrees(tree);

            var model = compilation.GetSemanticModel(tree);

            var literalInfo = model.GetTypeInfo(helloWorldString);

            //可获取具体的类型
            var stringTypeSymbol = (INamedTypeSymbol)literalInfo.Type;

            Console.WriteLine("method:--------------------------------------------------");
            foreach (var name in (from method in stringTypeSymbol.GetMembers()
                                                              .OfType<IMethodSymbol>()
                                  where method.ReturnType.Equals(stringTypeSymbol) &&
                                        method.DeclaredAccessibility ==
                                                   Accessibility.Public
                                  select method.Name).Distinct())
            {
                Console.WriteLine(name);
            }

        }
    }
}
