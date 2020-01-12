
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Evans.XamlTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CodeEditor: ContentView
    {
        public static BindableProperty labelProperty = 
            BindableProperty.Create(nameof(label), typeof(string), typeof(CodeEditor), default, BindingMode.TwoWay);
        public static BindableProperty codeProperty = 
            BindableProperty.Create(nameof(code), typeof(string), typeof(CodeEditor), default, BindingMode.TwoWay);

        public CodeEditor()
        {
            InitializeComponent();
            _Label.BindingContext = this;
            _Label.SetBinding(Label.TextProperty,nameof(label));
            _Entry.BindingContext = this;
            _Entry.SetBinding(Entry.TextProperty,nameof(code));

        }
        public string label { get => (string)GetValue(labelProperty); set => SetValue(labelProperty, value); }
        public string code { get => (string)GetValue(codeProperty); set => SetValue(codeProperty, value); }

    }
}