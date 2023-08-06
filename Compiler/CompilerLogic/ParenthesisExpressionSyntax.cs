namespace Compiler.CompilerLogic
{
    sealed class ParenthesisExpressionSyntax : ExpressionSyntax{
        public ParenthesisExpressionSyntax(SyntaxToken openbracketToken, ExpressionSyntax expression, SyntaxToken closebracketToken){
            OpenBracketToken = openbracketToken;
            Expression = expression;
            ClosebracketToken = closebracketToken;
        }
        public SyntaxToken OpenBracketToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken ClosebracketToken { get; }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

        public override IEnumerable<SyntaxNode> GetChildren(){
            yield return OpenBracketToken;
            yield return Expression;
            yield return ClosebracketToken;
        }
    }
}