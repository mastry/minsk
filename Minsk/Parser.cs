using System.Collections.Generic;

namespace Minsk
{
    class Parser
    {
        private readonly Token[] _tokens;

        private List<string> _diagnostics = new List<string>();
        private int _position;

        public Parser(string text)
        {
            var tokens = new List<Token>();

            var lexer = new Lexer(text);
            Token token;
            do
            {
                token = lexer.NextToken();

                if (token.Kind != TokenType.Whitespace &&
                    token.Kind != TokenType.BadToken)
                {
                    tokens.Add(token);
                }
            } while (token.Kind != TokenType.EOF);

            _tokens = tokens.ToArray();
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private Token Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];

            return _tokens[index];
        }

        private Token Current => Peek(0);

        private Token NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private Token Match(TokenType kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected <{kind}>");
            return new Token(kind, Current.Position, string.Empty, null);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }

        public SyntaxTree Parse()
        {
            var expresion = ParseTerm();
            var endOfFileToken = Match(TokenType.EOF);
            return new SyntaxTree(_diagnostics, expresion, endOfFileToken);
        }

        private ExpressionSyntax ParseTerm()
        {
            var left = ParseFactor();

            while (Current.Kind == TokenType.Plus ||
                   Current.Kind == TokenType.Minus)
            {
                var operatorToken = NextToken();
                var right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParseFactor()
        {
            var left = ParsePrimaryExpression();

            while (Current.Kind == TokenType.Star ||
                   Current.Kind == TokenType.Slash)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == TokenType.LeftParens)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = Match(TokenType.RightParens);
                return new ParenthesizedExpressionSyntax(left, expression, right);
            }

            var integerToken = Match(TokenType.Integer);
            return new NumberExpressionSyntax(integerToken);
        }
    }
}