using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Evans.XamlTemplates
{
    public class Control : Node
    {
        public Control(Token token, XElement node) : base(token)
        {
            Node = node;
        }

        //public List<Control> ChildControls { get; set; } = new List<Control>();

        public string Namespace => Name.NamespaceName;//Node.GetDefaultNamespace().NamespaceName;

        public XName Name => Node.Name;

        public List<ControlProperty> ControlProperties { get; set; } = new List<ControlProperty>();

        public bool HasParameter => ControlProperties.Any(p => p.IsParameter);
        public XElement Node { get; set; }
    }
}