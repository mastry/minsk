using System.Collections.Generic;

namespace Minsk
{
    public abstract class SyntaxNode
    {
        public abstract TokenType Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}