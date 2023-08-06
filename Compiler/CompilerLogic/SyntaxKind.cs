namespace Compiler.CompilerLogic
{
    
    //Enum type for different Tokens and Expressions(Asortments of tokens)
    enum SyntaxKind
    {
        NumberToken,
        WhitespaceToken,
        PlusToken,
        MinusToken,
        AsterixToken,
        ForwardslashToken,
        OpenbracketToken,
        ClosebracketToken,
        BadToken,
        EndToken,
        NumberExpression,
        BinaryExpression,
        ParenthesizedExpression
    }
}