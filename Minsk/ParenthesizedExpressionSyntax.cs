using System.Collections.Generic;

namespace Minsk
{
    sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(Token openParenthesisToken, ExpressionSyntax expression, Token closeParenthesisToken)
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }

        public override TokenType Kind => TokenType.ParenthesizedExpression;
        public Token OpenParenthesisToken { get; }
        public ExpressionSyntax Expression { get; }
        public Token CloseParenthesisToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return CloseParenthesisToken;
        }
    }
}