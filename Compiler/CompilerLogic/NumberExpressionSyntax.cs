namespace Compiler.CompilerLogic
{
    sealed class NumberExpressionSyntax : ExpressionSyntax{
        public NumberExpressionSyntax(SyntaxToken numberToken){
                NumberToken = numberToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public SyntaxToken NumberToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            //Yield used to return items one by one to IEnumerable<t>
            yield return NumberToken;
        }
    }
}