namespace Evans.XamlTemplates
{
    public class Token
    {
        public Token(TokenType tokenType, int index, int line, string? value = null)
        {
            TokenType = tokenType;
            Index = index;
            Value = value;
            Line = line;
        }

        public Token()
        {
            
        }

        public int Line { get; set; }
        public TokenType TokenType { get; set; }
        public string? Value { get; set; }
        public int Index { get; set; }
        public override string ToString()
        {
            return $"{TokenType}:{Value}";
        }
    }
}