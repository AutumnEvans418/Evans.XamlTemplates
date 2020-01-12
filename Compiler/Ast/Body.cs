using System.Collections.Generic;
using System.Xml;

namespace Evans.XamlTemplates
{
    public class Body : Node
    {
        public Body(Token token, XmlDocument xml) : base(token)
        {
            Xml = xml;
        }
        public XmlDocument Xml { get; set; }
        public List<Control> Controls { get; set; } = new List<Control>();
    }
}