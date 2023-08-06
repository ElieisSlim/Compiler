namespace Compiler.CompilerLogic
{
    class Parser
    {
        private readonly SyntaxToken[] _tokens;

        //error list
        private List<string> _diagnostics = new List<string>();
        private int _position;
        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            
            var lexer = new Lexer(text);
            SyntaxToken token;
            do{
                token = lexer.NextToken();

                if(token.Kind != SyntaxKind.WhitespaceToken &&
                    token.Kind != SyntaxKind.BadToken){
                        tokens.Add(token);
                    }
            } while (token.Kind != SyntaxKind.EndToken);
            //Above code adds to tokens list using the Lexer class as long as they are not bad tokens or whitespace

            _tokens = tokens.ToArray();
            //convert tokens list to array
            _diagnostics.AddRange(lexer.Diagnostics);
            //add any lexing errors to the error list
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private SyntaxToken Peek(int offset){
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];

            return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken(){
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken Match(SyntaxKind kind){
            if (Current.Kind == kind)
                return NextToken();
            _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        private ExpressionSyntax ParseExpression(){
            return ParseTerm();
        }

        public SyntaxTree Parse(){
            var expression = ParseTerm();
            var endToken = Match(SyntaxKind.EndToken);
            return new SyntaxTree(_diagnostics, expression, endToken);
        }


        //Handle Addition and Subtraction
        public ExpressionSyntax ParseTerm(){
            var left = ParseFactor();

            while (Current.Kind == SyntaxKind.PlusToken ||
                Current.Kind == SyntaxKind.MinusToken){
                    var operatorToken = NextToken();
                    var right = ParseFactor();
                    left = new BinaryExpressionSyntax(left, operatorToken, right);
                }

                return left;
        }

        //Handle Multiplication and Division        
        public ExpressionSyntax ParseFactor(){
            var left = ParsePrimaryExpression();

            while (Current.Kind == SyntaxKind.AsterixToken ||
                Current.Kind == SyntaxKind.ForwardslashToken){
                    var operatorToken = NextToken();
                    var right = ParsePrimaryExpression();
                    left = new BinaryExpressionSyntax(left, operatorToken, right);
                }

                return left;
        }

        //Handle Parenthesis
        private ExpressionSyntax ParsePrimaryExpression(){
            if(Current.Kind == SyntaxKind.OpenbracketToken){
                var left = NextToken();
                var expression = ParseExpression();
                var right = Match(SyntaxKind.ClosebracketToken);
                return new ParenthesisExpressionSyntax(left, expression, right);
            }


            var numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
}