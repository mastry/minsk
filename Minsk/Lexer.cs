using System;

namespace Minsk
{
    public class Lexer
    {
        readonly string _text;
        int _position;

        public Lexer(string text) => _text = text;

        private char Current => _position < _text.Length ? _text[_position] : '\0';

        private void Next() => _position++;

        public SyntaxToken NextToken()
        {
            if (_position >= _text.Length)
                return new SyntaxToken(TokenType.EOF, _position, "\0", null);

            if (char.IsDigit(Current))
            {
                var start = _position;

                while (char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);
                return new SyntaxToken(TokenType.Number, start, text, value);
            }

            else if (char.IsWhiteSpace(Current))
            {
                var start = _position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(TokenType.Whitespace, start, text, null);
            }

            else if (Current == '+')
                return new SyntaxToken(TokenType.Plus, _position++, "+", null);
            else if (Current == '-')
                return new SyntaxToken(TokenType.Minus, _position++, "-", null);
            else if (Current == '*')
                return new SyntaxToken(TokenType.Star, _position++, "*", null);
            else if (Current == '/')
                return new SyntaxToken(TokenType.Slash, _position++, "/", null);
            else if (Current == '(')
                return new SyntaxToken(TokenType.LeftParens, _position++, "(", null);
            else if (Current == ')')
                return new SyntaxToken(TokenType.RightParens, _position++, ")", null);

            return new SyntaxToken(TokenType.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }
    }
}
