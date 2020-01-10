using System.Collections.Generic;

namespace Evans.XamlTemplates
{
    public enum Token
    {
        At,
        Id,
        ParenthesesOpen,
        ParenthesesClose,
        BracketOpen,
        BracketClose,
        Comma,
        ForwardSlash,
        Quote

    }

    //public class TamlParser
    //{
    //    public int Index { get; set; }
    //    public IList<char> Characters { get; set; } = new List<char>();

    //    public char? Peek(int offset = 0)
    //    {
    //        if (Characters.Count > Index + offset)
    //            return Characters[Index + offset];
    //        return null;
    //    }

    //    public void Move()
    //    {
    //        Index++;
    //    }

    //    public IEnumerable<Token> GetTokens(string code)
    //    {
    //        Characters = new List<char>(code);
    //        Index = 0;
    //        while (Peek() != null)
    //        {
                
    //        }

    //    }
    //}
}