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
            while (Peek() is char val)
            {
                if (val == '@')
                {
                    Tokens.Add(Token.At);
                    Move();
                }
                else if (char.IsLetter(val))
                {
                    var id = "";
                    while (Peek() is char c && char.IsLetter(c))
                    {
                        id += c;
                    }
                    Tokens.Add(Token.Id);
                }
                
            }

        }
    }
}