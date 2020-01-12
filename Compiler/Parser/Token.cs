namespace Evans.XamlTemplates
{
    public class Token
    {
        public Token(TokenType tokenType, int index, string? value = null)
        {
            TokenType = tokenType;
            Index = index;
            Value = value;
        }

        public Token()
        {
            
        }
        public TokenType TokenType { get; set; }
        public string? Value { get; set; }
        public int Index { get; set; }
        public override string ToString()
        {
            return $"{TokenType}:{Value}";
        }
    }
}