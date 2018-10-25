using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysisDemo
{
    public class AnalysisDemo
    {
        /// <summary>
        ///解析语法树
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SyntaxNode GetRoot(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);
            //SyntaxTree的根root
            var root = (CompilationUnitSyntax)tree.GetRoot();
            //member
            var firstmember = root.Members[0];
            //命名空间Namespace
            var helloWorldDeclaration = (NamespaceDeclarationSyntax)firstmember;
            //类 class
            var programDeclaration = (ClassDeclarationSyntax)helloWorldDeclaration.Members[0];
            //方法 Method
            var mainDeclaration = (MethodDeclarationSyntax)programDeclaration.Members[0];
            //参数 Parameter
            var argsParameter = mainDeclaration.ParameterList.Parameters[0];

            //查询方法，查询方法名称为Main的第一个参数。
            var firstParameters = from methodDeclaration in root.DescendantNodes()
                                                    .OfType<MethodDeclarationSyntax>()
                                  where methodDeclaration.Identifier.ValueText == "Main"
                                  select methodDeclaration.ParameterList.Parameters.First();

            var argsParameter2 = firstParameters.Single();
            return root;
        }
        /// <summary>
        /// 演示CSharpSyntaxWalker
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public UsingCollector GetCollector(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var collector = new UsingCollector();
            collector.Visit(root);
            return collector;
        }
    }
    /// <summary>
    /// 收集器
    /// </summary>
    public class UsingCollector : CSharpSyntaxWalker
    {
        public readonly Dictionary<string, List<string>> models = new Dictionary<string, List<string>>();
        public readonly List<UsingDirectiveSyntax> Usings = new List<UsingDirectiveSyntax>();

        public override void VisitUsingDirective(UsingDirectiveSyntax node) {
            if (node.Name.ToString() != "System" &&
               !node.Name.ToString().StartsWith("System."))
            {
                this.Usings.Add(node);
            }
        }
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var classnode = node.Parent as ClassDeclarationSyntax;
            if (!models.ContainsKey(classnode.Identifier.ValueText))
            {
                models.Add(classnode.Identifier.ValueText, new List<string>());
            }
            models[classnode.Identifier.ValueText].Add(node.Identifier.ValueText);
        }

    }
}
