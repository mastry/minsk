using System;

namespace Minsk
{
    public enum TokenType
    {
        Number,
        Whitespace,
        Plus,
        Minus,
        Star,
        Slash,
        LeftParens,
        RightParens,
        BadToken,
        EOF,
        Identifier,
        Integer,
        Double
    }
}