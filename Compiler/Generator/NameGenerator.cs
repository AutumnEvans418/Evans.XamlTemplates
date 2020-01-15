using System.Collections.Generic;
using System.Linq;

namespace Evans.XamlTemplates.Generator
{
    public class NameGenerator
    {
        void RecurseControls(IEnumerable<Control> controls)
        {
            foreach (var control in controls)
            {
                if (control.HasParameter)
                {
                    if (control.Node.Attributes != null && control.Node.OwnerDocument != null)
                    {

                        foreach (var property in control.ControlProperties.Where(p => p.IsParameter))
                        {
                            control.Node.Attributes.Remove(control.Node.Attributes[property.Name]);

                        }

                        //control.Node.Attributes.RemoveAll();
                        var att = control.Node.OwnerDocument.CreateAttribute("x", "Name", "http://schemas.microsoft.com/winfx/2009/xaml");
                        att.Value = AddControl(control);
                        control.Node.Attributes.Append(att);
                    }
                }
                RecurseControls(control.ChildControls);
            }
        }
        public NameGenerator(IEnumerable<Control> controls)
        {
            RecurseControls(controls);
            //map the types and names here instead of the xamlgenerator.  That way it doesn't matter which is run first
        }
        Dictionary<string, int> controlPrefix = new Dictionary<string, int>();
        public Dictionary<string, Control> NamedControls { get; set; } = new Dictionary<string, Control>();


        string RemoveColon(string name)
        {
            return name.Contains(":") ? name.Split(':')[1] : name;
        }

        public string AddControl(Control control)
        {
            var name = "_" + RemoveColon(control.Name);

            if (NamedControls.ContainsKey(name))
            {
                if (controlPrefix.ContainsKey(name))
                {
                    controlPrefix[name]++;
                    name += controlPrefix[name];
                    NamedControls.Add(name, control);
                }
                else
                {
                    controlPrefix.Add(name, 1);
                    name += controlPrefix[name];
                    NamedControls.Add(name, control);
                }
            }
            else
            {
                NamedControls.Add(name, control);
            }
            return name;
        }
    }
}