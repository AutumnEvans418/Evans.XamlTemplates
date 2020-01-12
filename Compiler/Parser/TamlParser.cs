using System;
using System.Collections.Generic;
using System.Linq;

namespace Evans.XamlTemplates
{
    public class TamlParser : Iterator<char?>
    {
        public IList<Token> Output { get; set; } = new List<Token>();

        public void Add(TokenType type, string val = null)
        {
            Output.Add(new Token(type, val));
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
            while (Peek() is char val)
            {
                if (char.IsWhiteSpace(val))
                {
                    Move();
                }
                else if (val == '@')
                {
                    Add(TokenType.At);
                }
                else if (char.IsLetter(val))
                {
                    var id = "";
                    while (Peek() is char c && char.IsLetter(c))
                    {
                        id += c;
                        Move();
                    }
                    Output.Add(new Token(TokenType.Id, id));
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
                    Output.Add(new Token(TokenType.Quote, q));
                }
                else
                {
                    throw new InvalidOperationException($"Did not recognize token {val}");
                }
                
            }
            Output.Add(new Token(TokenType.EndOfFile));

            return Output;
        }
    }
}