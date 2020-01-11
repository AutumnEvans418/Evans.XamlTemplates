using System.Collections.Generic;

namespace Evans.XamlTemplates.Generator
{
    public class NameGenerator
    {

        public NameGenerator()
        {
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