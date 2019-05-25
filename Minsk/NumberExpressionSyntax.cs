using System.Collections.Generic;

namespace Minsk
{
    public sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        public NumberExpressionSyntax(Token numberToken)
        {
            NumberToken = numberToken;
        }

        public override TokenType Kind => TokenType.NumberExpression;
        public Token NumberToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }
}