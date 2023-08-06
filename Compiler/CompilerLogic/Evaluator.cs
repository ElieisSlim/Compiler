namespace Compiler.CompilerLogic
{
    class Evaluator
    {
        //This calss is for calculating the parsed tokens
        private readonly ExpressionSyntax _root;
        public Evaluator(ExpressionSyntax root){
            this._root = root;
        }

        public int Evaluate(){
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(ExpressionSyntax node)
        {
            //NumberExpression found -> returns a number token
            if (node is NumberExpressionSyntax n)
                return (int) n.NumberToken.Value;
            //Binary expression found -> operate on left and right based on maths token used
            if (node is BinaryExpressionSyntax b){
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);
                return b.OperatorToken.Kind switch
                {
                    SyntaxKind.PlusToken => left + right,
                    SyntaxKind.MinusToken => left - right,
                    SyntaxKind.AsterixToken => left * right,
                    SyntaxKind.ForwardslashToken => left / right,
                    _ => throw new Exception($"Unexpected operator {b.OperatorToken.Kind}"),
                };
            }

            //RETURN THE EXPRESSION IN PARENTHESIS
            if (node is ParenthesisExpressionSyntax p)
                return EvaluateExpression(p.Expression);

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}