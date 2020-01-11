using System;
using System.Collections.Generic;
using System.Linq;

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
        CurlyBracketOpen,
        CurlyBracketClose,
        Equal
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

    public class Iterator<T>
    {
        public int Index { get; set; }
        public IList<T> Input { get; set; } = new List<T>();

        public T Peek(int offset = 0)
        {
            if (Input.Count > Index + offset)
                return Input[Index + offset];
            return default;
        }

        public void Move()
        {
            Index++;
        }
    }

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

            return Output;
        }
    }

    public enum AstType
    {
        ClassName,
        Parameter,
        ControlName,
        ControlProperty,
        ParameterCall,
    }

    public class Node
    {
        public Node(AstType astType, Token token)
        {
            AstType = astType;
            Token = token;
        }

        public AstType AstType { get; set; }

        public Token Token { get; set; }

    }

    public class TamlAst : Iterator<Token>
    {
        public void Eat(TokenType token)
        {
            var p = Peek();
            if (p == null)
            {
                throw new InvalidOperationException($"Expected {token} but was null");
            }
            if (p is Token t && t.TokenType == token)
            {
                throw new InvalidOperationException($"Expected {token} but was {t.TokenType}");
            }
            Move();
        }

        public Node Evaluate(IList<Token> tokens)
        {
            Input = tokens;
            Eat(TokenType.At);
            
        }
    }
}