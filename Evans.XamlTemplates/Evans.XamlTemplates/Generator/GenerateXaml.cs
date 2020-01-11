using System.Collections.Generic;
using System.Reflection;

namespace Evans.XamlTemplates.Generator
{

    public class GeneratedFile
    {
        public string FileName { get; set; } = "";
        public string Content { get; set; } = "";
    }
    public class GeneratedType
    {
        public GeneratedType(GeneratedFile xaml, GeneratedFile cSharp)
        {
            Xaml = xaml;
            CSharp = cSharp;
        }

        public GeneratedFile Xaml { get; set; }
        public GeneratedFile CSharp { get; set; }
    }
    public class Generator
    {
        public IEnumerable<GeneratedType> Generate(Program program)
        {
            var xaml = new GenerateXaml();
            var csharp = new GenerateCSharp();
            foreach (var programTemplate in program.Templates)
            {
                yield return new GeneratedType(xaml.Generate(programTemplate),csharp.Generate(programTemplate));
            }
        }
    }
    public class GenerateCSharp
    {
        public string Result { get; set; }

        public GeneratedFile Generate(Template template)
        {
            Result = "";
            var name = template.ClassName;
            var file = new GeneratedFile();

            file.FileName = name + ".xaml.cs";
            file.Content = Result;
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

        Dictionary<string, int> controlPrefix = new Dictionary<string, int>();
        Dictionary<string, Control> namedControls = new Dictionary<string, Control>();
        string GetName(Control control)
        {

        }
        string AddNamesToXml(Body? body)
        {
            if (body == null) return "";
            RecurseControls(body.Controls);
            return body.Xml.OuterXml;
        }

        void RecurseControls(IEnumerable<Control> controls)
        {
            foreach (var control in controls)
            {
                if (control.HasParameter)
                {
                    if (control.Node.Attributes != null && control.Node.OwnerDocument != null)
                    {
                        control.Node.Attributes.RemoveAll();
                        var att = control.Node.OwnerDocument.CreateAttribute("x:Name");
                        att.Value = GetName(control);
                        control.Node.Attributes.Append(att);
                    }
                }
                RecurseControls(control.ChildControls);
            }
        }
        public Template Template { get; set; }
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