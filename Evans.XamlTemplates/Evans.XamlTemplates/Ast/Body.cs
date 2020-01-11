using System.Collections.Generic;

namespace Evans.XamlTemplates
{
    public class Body : Node
    {
        public Body(Token token) : base(token)
        {
        }
        public string Xml { get; set; } = "";
        public List<Control> Controls { get; set; } = new List<Control>();
    }
}