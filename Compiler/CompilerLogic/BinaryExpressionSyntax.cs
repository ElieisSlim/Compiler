namespace Compiler.CompilerLogic
{
    sealed class BinaryExpressionSyntax : ExpressionSyntax{
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right){
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public ExpressionSyntax Left { get; }
        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Right { get; }

        //SyntaxKind Becomes BinaryExpression
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            //yield return used to return i IEnumerable<T> set of values
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }

    }
}