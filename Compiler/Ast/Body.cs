using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Evans.XamlTemplates
{
    public class Body : Node
    {
        public Body(Token token, XmlDocument xml) : base(token)
        {
            Xml = xml;
        }

        public string FormattedXml => XDocument.Parse(Xml.OuterXml).ToString();

        public XmlDocument Xml { get; set; }
        public List<Control> Controls { get; set; } = new List<Control>();
    }
}