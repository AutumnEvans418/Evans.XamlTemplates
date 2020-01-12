using System.Collections.Generic;
using System.Linq;

namespace Evans.XamlTemplates
{
    public class SyntaxChecker
    {
        private readonly Program _program;

        public SyntaxChecker(Program program)
        {
            _program = program;
        }


        public void CheckSyntax()
        {
            foreach (var template in _program.Templates)
            {
                if (template.Body == null) throw new CompileException("Body was empty", template.Token.Index);
                CheckControlsAndParameters(template.Body.Controls, template.Parameters);
            }
        }

        public void CheckControlsAndParameters(List<Control> controls, List<Parameter> parameters)
        {
            foreach (var control in controls)
            {
                if (control.HasParameter)
                {
                    foreach (var property in control.ControlProperties.Where(p=>p.IsParameter))
                    {
                        if (parameters.Any(p => p.Name == property.Value.Substring(1)) != true)
                        {
                            throw new CompileException($"Could not find parameter {property.Value}", property.Token.Index);
                        }
                    }
                }
                CheckControlsAndParameters(control.ChildControls, parameters);
            }
        }
    }
}