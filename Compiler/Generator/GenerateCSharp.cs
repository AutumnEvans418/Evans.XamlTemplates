using System;
using System.Linq;
using System.Reflection;

namespace Evans.XamlTemplates.Generator
{
    public class GenerateCSharp
    {
        private string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        private string CSharpTemplate => @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace " + AssemblyName + @"
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class " + Template?.ClassName + @": ContentView
    {
        " + GenerateBindableProperties() + @"
        public " + Template?.ClassName + @"()
        {
            InitializeComponent();
            " + GenerateConstructor() + @"
        }
        " + GenerateProperties() + @"
    }
}";
        private string GenerateConstructor()
        {
            if (NameGenerator == null) return "";
            //var str = @"_Label.BindingContext = this;
            //_Entry.BindingContext = this;
            //_Label.SetBinding(Xamarin.Forms.Label.TextProperty,nameof(Label));
            //_Entry.SetBinding(Xamarin.Forms.Entry.TextProperty, nameof(Text));";
            var str = "";
            foreach (var control in NameGenerator.NamedControls)
            {
                str += $"{control.Key}.BindingContext = this;{Environment.NewLine}";

                foreach (var property in control.Value.ControlProperties.Where(p=>p.IsParameter))
                {
                    str += $"{control.Key}.SetBinding(Xamarin.Forms.{control.Value.Name}.{property.Name}Property,nameof({property.Value.Substring(1)}));{Environment.NewLine}";
                }
            }

            return str;
        }
        private string GenerateProperties()
        {
            if (Template == null) return "";
            var parameters = Template.Parameters;
            var str = "";

            foreach (var parameter in parameters)
            {
                str += @"public " + parameter.Type + @" " + parameter.Name + @" { get => (" + parameter.Type + @")GetValue(" + parameter.Name + @"Property); set => SetValue(" + parameter.Name + @"Property, value); }
";
            }

            return str;
        }

        string GenerateBindableProperties()
        {
            if (Template == null) return "";
            var parameters = Template.Parameters;
            var str = "";

            foreach (var parameter in parameters)
            {
                str += $@"public static BindableProperty {parameter.Name}Property = 
            BindableProperty.Create(nameof({parameter.Name}), typeof({parameter.Type}), typeof({Template?.ClassName}), default, BindingMode.TwoWay);
";
            }

            return str;
        }

        public Template? Template { get; set; }
        public NameGenerator? NameGenerator { get; set; }
        public GeneratedFile Generate(Template template, NameGenerator nameGenerator)
        {
            Template = template;
            NameGenerator = nameGenerator;
            var name = template.ClassName;
            var file = new GeneratedFile();

            file.FileName = name + ".xaml.cs";
            file.Content = CSharpTemplate;
            return file;
        }
    }
}