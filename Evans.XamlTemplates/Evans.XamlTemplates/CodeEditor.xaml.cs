
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
        public static BindableProperty CodeProperty = 
            BindableProperty.Create(nameof(Code), typeof(string), typeof(CodeEditor), default, BindingMode.TwoWay);
        public static BindableProperty VisibleProperty = 
            BindableProperty.Create(nameof(Visible), typeof(bool), typeof(CodeEditor), default, BindingMode.TwoWay);

        public CodeEditor()
        {
            InitializeComponent();
            _Label.BindingContext = this;
            _Label.SetBinding(Label.TextProperty,nameof(Code));
            _Label.SetBinding(Label.IsVisibleProperty,nameof(Visible));

        }
        public string Code { get => (string)GetValue(CodeProperty); set => SetValue(CodeProperty, value); }
        public bool Visible { get => (bool)GetValue(VisibleProperty); set => SetValue(VisibleProperty, value); }

    }
}