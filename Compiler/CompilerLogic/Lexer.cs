namespace Compiler.CompilerLogic
{
    class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();
        public Lexer(string text){
            _text = text;
        }
        
        //error list
        public IEnumerable<string> Diagnostics => _diagnostics;
        
        private char Current
        {
            get{
                if(_position
                    >= _text.Length)
                return '\0';
            return _text[_position];
            }
        }

        

        private void Next(){
            _position++;
        }       

        public SyntaxToken NextToken(){
            //NextToken can currently handle numerical digits whitespace and 'SyntaxKind' operators "+-*/()"

            if(_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EndToken, _position, "\0", null);
            

            if(char.IsDigit(Current)){
                var start = _position;

                while (char.IsDigit(Current))
                    Next();
                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value)){
                    _diagnostics.Add($"The number {_text} cannot be represented");
                }
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current)){
                var start = _position;

                while (char.IsWhiteSpace(Current))
                    Next();
                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, value);
            }

            if (Current == '+')
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
            else if (Current == '-')
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            else if (Current == '*')
                return new SyntaxToken(SyntaxKind.AsterixToken, _position++, "*", null);
            else if (Current == '/')
                return new SyntaxToken(SyntaxKind.ForwardslashToken, _position++, "/", null);
            else if (Current == '(')
                return new SyntaxToken(SyntaxKind.OpenbracketToken, _position++, "(", null);
            else if (Current == ')')
                return new SyntaxToken(SyntaxKind.ClosebracketToken, _position++, ")", null);

            //Error Handling for unrecognised tokens
            _diagnostics.Add($"ERROR: bad token: '{Current}'");
            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);    
        }

    }
}