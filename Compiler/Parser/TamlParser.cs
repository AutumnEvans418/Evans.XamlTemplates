using System;
using System.Collections.Generic;
using System.Linq;

namespace Evans.XamlTemplates
{
    public class TamlParser : Iterator<char?>
    {
        public IList<Token> Output { get; set; } = new List<Token>();

        public void Add(TokenType type, string? val = null)
        {
            Output.Add(new Token(type, Index, val));
            Move();
        }
        public IEnumerable<Token> GetTokens(string code)
        {
            Input = new List<char?>();
            foreach (var c in code)
            {
                Input.Add(c);
            }
            Index = 0;
            Output = new List<Token>();
            while (Peek() is { } val)
            {
                if (val == '/' && Peek(1) == '/')
                {
                    Move();
                    Move();
                    while (Peek() is {} c && c != '\n')
                    {
                        Move();
                    }
                }else if (char.IsWhiteSpace(val))
                {
                    Move();
                }
                else if (val == '@')
                {
                    Add(TokenType.At,val.ToString());
                }
                else if (val == '.')
                {
                    Add(TokenType.Period, val.ToString());
                }
                else if (char.IsLetter(val))
                {
                    var id = "";
                    while (Peek() is { } c && (char.IsLetter(c) || char.IsNumber(c) || c == '_'))
                    {
                        id += c;
                        Move();
                    }
                    Output.Add(new Token(TokenType.Id, Index, id));
                }
                else if (val == '(')
                {
                    Add(TokenType.ParenthesesOpen, val.ToString());
                }
                else if (val == ')')
                {
                    Add(TokenType.ParenthesesClose, val.ToString());
                }
                else if (val == ':')
                {
                    Add(TokenType.Colon, val.ToString());
                }
                else if (val == ',')
                {
                    Add(TokenType.Comma, val.ToString());
                }
                else if (val == '{')
                {
                    Add(TokenType.CurlyBracketOpen, val.ToString());
                }
                else if (val == '}')
                {
                    Add(TokenType.CurlyBracketClose, val.ToString());
                }
                else if (val == '/')
                {
                   Add(TokenType.ForwardSlash, val.ToString());
                }
                else if (val == '<')
                {
                    Add(TokenType.BracketOpen, val.ToString());
                }
                else if (val == '>')
                {
                    Add(TokenType.BracketClose, val.ToString());
                }
                else if (val == '=')
                {
                    Add(TokenType.Equal, val.ToString());
                }
                else if (val == '"')
                {
                    var q = "";
                    Move();
                    while (Peek() is char c && c != '"')
                    {
                        q += c;
                        Move();
                    }
                    Move();
                    Output.Add(new Token(TokenType.Quote, Index, q));
                }
                else
                {
                    throw new CompileException($"Did not recognize token {val}", Index);
                }
                
            }
            Output.Add(new Token(TokenType.EndOfFile, Index));

            return Output;
        }
    }
}