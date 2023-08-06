using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Compiler.CompilerLogic
{
    abstract class SyntaxNode{
        public abstract SyntaxKind Kind { get; }
        public abstract IEnumerable<SyntaxNode> GetChildren();
        
    }
}