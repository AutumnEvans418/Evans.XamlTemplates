using System.Collections.Generic;

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
        public string AddControl(Control control)
        {
            var name = "_" + control.Name;
            if (NamedControls.ContainsKey(name))
            {
                if (controlPrefix.ContainsKey(name))
                {
                    controlPrefix[name]++;
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