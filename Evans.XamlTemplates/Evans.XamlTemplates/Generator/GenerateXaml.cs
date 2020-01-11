using System.Collections.Generic;
using System.Reflection;

namespace Evans.XamlTemplates.Generator
{
    public class GenerateCSharp
    {
        private string CSharpTemplate = @"
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Evans.XamlTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LabelEntry : ContentView
    {
        public static BindableProperty LabelProperty = 
            BindableProperty.Create(nameof(Label), typeof(string), typeof(LabelEntry), default, BindingMode.TwoWay);
        public static BindableProperty TextProperty = 
            BindableProperty.Create(nameof(Label), typeof(string), typeof(LabelEntry), default, BindingMode.TwoWay);
        public LabelEntry()
        {
            InitializeComponent();
            _Label.BindingContext = this;
            _Entry.BindingContext = this;
            _Label.SetBinding(Xamarin.Forms.Label.TextProperty,nameof(Label));
            _Entry.SetBinding(Xamarin.Forms.Entry.TextProperty, nameof(Text));
        }
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        } 
    }
}";


        private readonly NameGenerator _nameGenerator;

        public GenerateCSharp(NameGenerator nameGenerator)
        {
            _nameGenerator = nameGenerator;
        }
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
        private readonly NameGenerator _nameGenerator;
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

        public GenerateXaml(NameGenerator nameGenerator)
        {
            _nameGenerator = nameGenerator;
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
                        //control.Node.Attributes.RemoveAll();
                        var att = control.Node.OwnerDocument.CreateAttribute("x","Name", "http://schemas.microsoft.com/winfx/2009/xaml");
                        att.Value = _nameGenerator.AddControl(control);
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