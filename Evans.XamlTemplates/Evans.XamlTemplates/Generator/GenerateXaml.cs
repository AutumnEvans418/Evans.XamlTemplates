using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Evans.XamlTemplates.Generator
{
    

    public class GenerateCSharp
    {
        private string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        private string CSharpTemplate => @"
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace "+ AssemblyName + @"
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class" + Template?.ClassName + @": ContentView
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
                    str += $"{control.Key}.SetBinding(Xamarin.Forms.{property.Name}Property,nameof({property.Value.Substring(1)}));{Environment.NewLine}";
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
                str += @"public " + parameter.Type + @" Label { get => (" + parameter.Type + @")GetValue(" + parameter.Name + @"Property); set => SetValue(" + parameter.Name + @"Property, value); }
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

    public class GenerateXaml
    {
        private string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
        private string XamlTemplate => $@"
<?xml version=""1.0"" encoding=""UTF-8""?>
<ContentView xmlns=""http://xamarin.com/schemas/2014/forms"" 
             xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
             xmlns:d=""http://xamarin.com/schemas/2014/forms/design""
             xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
             mc:Ignorable=""d""
             x:Class=""{AssemblyName}.{Template.ClassName}"">
  <ContentView.Content>
      {AddNamesToXml(Template.Body)}
    </ContentView.Content>
</ContentView>";

        
        string AddNamesToXml(Body? body)
        {
            if (body == null) return "";
            return body.Xml.OuterXml;
        }

        
        public Template? Template { get; set; }
        public GeneratedFile Generate(Template template)
        {
            Template = template;
            var name = template.ClassName;
            var file = new GeneratedFile();
            
            file.FileName = name + ".xaml";
            file.Content = XamlTemplate;
            return file;
        }
    }
}