using System;

namespace Minsk
{
    public class Token
    {
        public Token(TokenType type, int position, string text, object? value)
        {
            TokenType = type;
            Position = position;
            Text = text;
            Value = value;
        }

        public TokenType TokenType { get; }
        public int Position { get; }
        public string Text { get; }
        public object? Value { get; }

        public override string ToString()
        {
            return $"{this.TokenType}: '{this.Text}' {(this.Value == null ? "" : $"({this.Value})")}";
        }
    }
}