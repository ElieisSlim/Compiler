using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

using Compiler.CompilerLogic;

namespace Compiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool showTree = false;
            while(true){
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)){
                    return;
                }
                if (line == "#showTree"){
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees");
                    continue;
                }
                var syntaxTree = SyntaxTree.Parse(line);
                if (showTree){
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    TreePrint(syntaxTree.Root);
                    Console.ForegroundColor = color;
                }
                else if (line == "#cls"){
                    Console.Clear();
                    continue;
                }

                if(!syntaxTree.Diagnostics.Any()){
                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }
                else{
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach(var item in syntaxTree.Diagnostics)
                        Console.WriteLine(item);
                    Console.ForegroundColor = color;
                }          
            }
        }
        static void TreePrint(SyntaxNode node, string indent = "", bool isLast = true){
            var marker = isLast ? "└──" :"├──";
            
            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null){
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│   ";
            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                TreePrint(child, indent, child == lastChild);
        }

    }

}