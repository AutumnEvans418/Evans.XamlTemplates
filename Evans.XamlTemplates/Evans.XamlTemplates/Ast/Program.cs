using System.Collections.Generic;

namespace Evans.XamlTemplates
{
    public class Program : Node
    {
        public Program(Token token) : base(token)
        {
        }

        public List<Template> Templates { get; set; } = new List<Template>();

    }
}