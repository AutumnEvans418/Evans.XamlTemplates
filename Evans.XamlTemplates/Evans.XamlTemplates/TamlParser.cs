using System;
using System.Collections.Generic;

namespace Evans.XamlTemplates
{
    public enum TokenType
    {
        At,
        Id,
        ParenthesesOpen,
        ParenthesesClose,
        BracketOpen,
        BracketClose,
        Comma,
        ForwardSlash,
        Quote,
        Xml,
    }

    public class Token
    {
        public Token(TokenType tokenType, string value = null)
        {
            TokenType = tokenType;
            Value = value;
        }

        public TokenType TokenType { get; set; }
        public string Value { get; set; }
    }

    public class TamlParser
    {
        public int Index { get; set; }
        public IList<char> Characters { get; set; } = new List<char>(); 
        public IList<Token> Tokens { get; set; }

        public char? Peek(int offset = 0)
        {
            if (Characters.Count > Index + offset)
                return Characters[Index + offset];
            return null;
        }

        public void Move()
        {
            Index++;
        }

        public IEnumerable<Token> GetTokens(string code)
        {
            Characters = new List<char>(code);
            Index = 0;
            Tokens = new List<Token>();
            while (Peek() is char val)
            {
                if (char.IsWhiteSpace(val))
                {
                    Move();
                }
                else if (val == '@')
                {
                    Tokens.Add(new Token(TokenType.At));
                    Move();
                }
                else if (char.IsLetter(val))
                {
                    var id = "";
                    while (Peek() is char c && char.IsLetter(c))
                    {
                        id += c;
                        Move();
                    }
                    Tokens.Add(new Token(TokenType.Id, id));
                }
                else if (val == '(')
                {
                    Tokens.Add(new Token(TokenType.ParenthesesOpen, val.ToString()));
                    Move();
                }
                else if (val == ')')
                {
                    Tokens.Add(new Token(TokenType.ParenthesesClose, val.ToString()));
                }
                else if (val == ',')
                {
                    Tokens.Add(new Token(TokenType.Comma, val.ToString()));
                }
                else
                {
                    throw new InvalidOperationException($"Did not recognize token {val}");
                }
                
            }

            return Tokens;
        }
    }
}