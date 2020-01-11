namespace Evans.XamlTemplates
{
    public class ControlProperty : Node
    {
        public ControlProperty(Token token) : base(token)
        {
        }
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public bool IsParameter => Value.StartsWith("@");
    }
}