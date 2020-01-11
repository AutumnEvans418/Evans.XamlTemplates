using System.Collections.Generic;
using System.Linq;

namespace Evans.XamlTemplates
{
    public class Control : Node
    {
        public Control(Token token) : base(token)
        {
        }

        public string Name { get; set; } = "";

        public List<ControlProperty> ControlProperties { get; set; } = new List<ControlProperty>();

        public bool HasParameter => ControlProperties.Any(p => p.IsParameter);
    }
}