using System;
using System.Collections.Generic;

namespace Minsk
{
    public class Lexer
    {
        readonly string _text;
        int _position = 0;
        private List<string> _diagnostics = new List<string>();

        public Lexer(string text) => _text = text;

        public IEnumerable<string> Diagnostics => _diagnostics;

        private char Current => _position < _text.Length ? _text[_position] : '\0';

        private void Next() => _position++;

        public Token NextToken()
        {
            if (_position >= _text.Length)
                return new Token(TokenType.EOF, _position, "\0", null);

            var token = Current switch
            {
                _ when char.IsLetter(Current) || Current == '_' => Identifier(),
                _ when char.IsDigit(Current) => Number(),
                _ when char.IsWhiteSpace(Current) => Whitespace(),
                '+' => CharToken(TokenType.Plus),
                '-' => CharToken(TokenType.Minus),
                '*' => CharToken(TokenType.Star),
                '/' => CharToken(TokenType.Slash),
                '(' => CharToken(TokenType.LeftParens),
                ')' => CharToken(TokenType.RightParens),
                _ => CharToken(TokenType.BadToken)
            };

            return token;
        }

        private Token CharToken(TokenType tokenType)
        {
            var token = new Token(tokenType, _position, $"{Current}", null);
            _position++;
            return token;
        }

        private Token Whitespace()
        {
            var start = _position;

            while (char.IsWhiteSpace(Current))
                Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            return new Token(TokenType.Whitespace, start, text, null);
        }

        private Token Number()
        {
            var start = _position;

            while (IsNumberChar(Current))
                Next();

            var length = _position - start;
            var text = _text
                .Substring(start, length)
                .Replace("_", "");

            if( int.TryParse(text, out var intValue) )
            {
                return new Token(TokenType.Integer, start, text, intValue);
            }
            else if( double.TryParse(text, out var doubleVal) )
            {
                return new Token(TokenType.Double, start, text, doubleVal);
            }
            else
            {
                _diagnostics.Add($"Invalid token. Can't convert {text} to a number.");
                return new Token(TokenType.BadToken, start, text, null);
            }

            static bool IsNumberChar(char c)
            {
                return c != '\0' &&
                    (char.IsDigit(c) || c == '_' || c == '.');
            }
        }

        private Token Identifier()
        {
            var start = _position;

            while (char.IsLetterOrDigit(Current) || Current == '_')
                Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            return new Token(TokenType.Identifier, start, text, null);
        }
    }
}
