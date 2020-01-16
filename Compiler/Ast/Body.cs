using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Evans.XamlTemplates
{
    public class Body : Node
    {
        public Body(Token token, XDocument xml) : base(token)
        {
            Xml = xml;
        }

        public string? FormattedXml() => Xml.Root?.FirstNode.ToString();

        public List<string> GetAllAssemblies()
        {
            return GetAssembliesByControl(Controls);
        }

        List<string> GetAssembliesByControl(List<Control> controls)
        {
            var t = new List<string>();
            foreach (var control in controls)
            {
                t.Add(control.Namespace);
                //t.AddRange(GetAssembliesByControl(control.ChildControls));
            }
            return t.Distinct().ToList();
        }
        

        public XDocument Xml { get; set; }
        public List<Control> Controls { get; set; } = new List<Control>();
    }
}