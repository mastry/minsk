using System;

namespace mc
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                var lexer = new Minsk.Lexer(line);
                var token = lexer.NextToken();
                while (token.TokenType != Minsk.TokenType.EOF)
                {
                    System.Console.WriteLine(token);
                    token = lexer.NextToken();
                    System.Console.WriteLine();
                }
            }
        }
    }
}
