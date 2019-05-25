using System;
using System.Collections.Generic;
using System.Linq;

namespace Minsk
{
    public class Token : SyntaxNode
    {
        public Token(TokenType type, int position, string text, object? value)
        {
            Kind = type;
            Position = position;
            Text = text;
            Value = value;
        }

        public override TokenType Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object? Value { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        public override string ToString()
        {
            return $"{this.Kind}: '{this.Text}' {(this.Value == null ? "" : $"({this.Value})")}";
        }
    }
}