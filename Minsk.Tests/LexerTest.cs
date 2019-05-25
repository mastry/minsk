using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Minsk.Tests
{
    public class LexerTest
    {
        [Fact]
        public void Lexer_CanLex_SimpleInteger()
        {
            const string input = "123";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Value != null);
            Assert.Equal(123, (token.Value));
            Assert.Equal(TokenType.Integer, token.Kind);
        }

        [Fact]
        public void Lexer_CanLex_IntegerWithUnderscores()
        {
            const string input = "123_456_9";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Value != null);
            Assert.Equal(1234569, (token.Value));
            Assert.Equal(TokenType.Integer, token.Kind);
        }

        [Fact]
        public void Lexer_CanLex_SimpleDouble()
        {
            const string input = "123.01";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Value != null);
            Assert.Equal(123.01, (token.Value));
            Assert.Equal(TokenType.Double, token.Kind);
        }

        [Fact]
        public void Lexer_CanLex_DoubleWithUnderscores()
        {
            const string input = "123_220.012_344_122";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Value != null);
            Assert.Equal(123220.012344122, (token.Value));
            Assert.Equal(TokenType.Double, token.Kind);
        }

        [Fact]
        public void Lexer_CanLex_Whitespace()
        {
            const string input = " test  ";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Whitespace);
            Assert.Equal(" ", token.Text);
            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Identifier);
            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Whitespace);
            Assert.Equal("  ", token.Text);
        }

        [Fact]
        public void Lexer_CanLex_SimpleIdentifier()
        {
            const string input = "someWord";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Identifier);
            Assert.Equal(input, token.Text);
        }

        [Fact]
        public void Lexer_CanLex_IdentifierWithUnderscores()
        {
            const string input = "_some_word";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Identifier);
            Assert.Equal(input, token.Text);
        }

        [Fact]
        public void Lexer_CanLex_IdentifierWithNumbers()
        {
            const string input = "_some1_word12";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Identifier);
            Assert.Equal(input, token.Text);
        }

        [Fact]
        public void Lexer_CanLex_Plus()
        {
            const string input = "+";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Plus);
            Assert.Equal(input, token.Text);
        }

        [Fact]
        public void Lexer_CanLex_Minus()
        {
            const string input = "-";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Minus);
            Assert.Equal(input, token.Text);
        }

        [Fact]
        public void Lexer_CanLex_Star()
        {
            const string input = "*";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Star);
            Assert.Equal(input, token.Text);
        }

        [Fact]
        public void Lexer_CanLex_Slash()
        {
            const string input = "/";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Slash);
            Assert.Equal(input, token.Text);
        }

        [Fact]
        public void Lexer_CanLex_LeftParens()
        {
            const string input = "(";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.LeftParens);
            Assert.Equal(input, token.Text);
        }

        [Fact]
        public void Lexer_CanLex_RightParens()
        {
            const string input = ")";
            var lexer = new Lexer(input);
            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.RightParens);
            Assert.Equal(input, token.Text);
        }

        [Fact]
        public void Lexer_CanLex_SimpleArithmetic()
        {
            const string input = "(1 + 2) * 99/3";
            var lexer = new Lexer(input);

            var token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.LeftParens);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Integer);
            Assert.Equal(1, token.Value);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Whitespace);
            Assert.Equal(" ", token.Text);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Plus);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Whitespace);
            Assert.Equal(" ", token.Text);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Integer);
            Assert.Equal(2, token.Value);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.RightParens);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Whitespace);
            Assert.Equal(" ", token.Text);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Star);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Whitespace);
            Assert.Equal(" ", token.Text);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Integer);
            Assert.Equal(99, token.Value);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Slash);

            token = lexer.NextToken();
            Assert.True(token.Kind == TokenType.Integer);
            Assert.Equal(3, token.Value);
        }
    }
}
