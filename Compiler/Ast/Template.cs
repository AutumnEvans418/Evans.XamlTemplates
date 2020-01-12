using System.Collections.Generic;

namespace Evans.XamlTemplates
{
    public class Template : Node
    {
        public Template(Token token) : base(token)
        {
        }

        public string ClassName { get; set; } = "";
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
        public Body? Body { get; set; } 
    }
}