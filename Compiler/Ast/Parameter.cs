namespace Evans.XamlTemplates
{
    public class Parameter : Node
    {
        public Parameter(Token token) : base(token)
        {
        }
        public string Type { get; set; } = "";
        public string Name { get; set; } = "";
    }
}