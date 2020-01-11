using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Evans.XamlTemplates
{
    public class Control : Node
    {
        public Control(Token token, XmlNode node) : base(token)
        {
            Node = node;
        }

        public List<Control> ChildControls { get; set; } = new List<Control>();

        public string Name { get; set; } = "";

        public List<ControlProperty> ControlProperties { get; set; } = new List<ControlProperty>();

        public bool HasParameter => ControlProperties.Any(p => p.IsParameter);
        public XmlNode Node { get; set; }
    }
}