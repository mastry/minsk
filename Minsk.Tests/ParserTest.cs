using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Minsk.Tests
{
    public class ParserTest
    {
        [Theory]
        [InlineData(1,"+",1)]
        [InlineData(54, "-", 8907)]
        [InlineData(1556346, "*", 5436)]
        [InlineData(43, "/", 5)]
        public void Parser_CanParse_SimpleArithmetic(int left, string op, int right)
        {
            // We have a binary expression
            var syntaxTree = SyntaxTree.Parse($"{left} {op}  {right}");
            Assert.Empty(syntaxTree.Diagnostics);
            Assert.True(syntaxTree.Root.Kind == TokenType.BinaryExpression);
            Assert.NotEmpty(syntaxTree.Root.GetChildren());

            // The root node has 3 children
            var children = syntaxTree.Root.GetChildren();
            Assert.Equal(3, children.Count());

            // We can convert the root to a BinaryExpressionSyntax
            var binExpression = syntaxTree.Root as BinaryExpressionSyntax;
            Assert.NotNull(binExpression);

            // The left node is a number with the correct value
            var leftExpression = binExpression.Left as NumberExpressionSyntax;
            Assert.NotNull(leftExpression);
            Assert.Equal(left, (int)leftExpression.NumberToken.Value);

            // The right node is a number with the correct value
            var rightExpression = binExpression.Right as NumberExpressionSyntax;
            Assert.NotNull(rightExpression);
            Assert.Equal(right, (int)rightExpression.NumberToken.Value);

            // The operator is of the correct type
            var expected = op switch
            {
                "+" => TokenType.Plus,
                "-" => TokenType.Minus,
                "*" => TokenType.Star,
                "/" => TokenType.Slash,
                _ => throw new Exception("Unknown arithmetic operator")
            };
            Assert.Equal(expected, binExpression.OperatorToken.Kind);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("42")]
        [InlineData("034")]
        [InlineData("65_345")]
        [InlineData("12_21_0")]
        public void Parser_CanParse_PositiveIntegers(string input)
        {
            var syntaxTree = SyntaxTree.Parse(input);
            Assert.Empty(syntaxTree.Diagnostics);
            Assert.True(syntaxTree.Root.Kind == TokenType.NumberExpression);
            Assert.NotEmpty(syntaxTree.Root.GetChildren());

            var integer = syntaxTree.Root as NumberExpressionSyntax;
            Assert.Equal(int.Parse(input.Replace("_","")), (int)integer.NumberToken.Value);
        }
    }
}
