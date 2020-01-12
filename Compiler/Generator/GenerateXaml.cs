using System.Collections.Generic;
using System.Reflection;

namespace Evans.XamlTemplates.Generator
{
    public class GenerateXaml
    {
        public string AssemblyName { get; set; } = Assembly.GetCallingAssembly().GetName().Name;
        private string XamlTemplate => $@"<?xml version=""1.0"" encoding=""UTF-8""?>
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