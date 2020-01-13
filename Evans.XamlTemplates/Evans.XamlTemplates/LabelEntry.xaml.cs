
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
    public partial class LabelEntry: ContentView
    {
        public static BindableProperty labelProperty = 
            BindableProperty.Create(nameof(label), typeof(string), typeof(LabelEntry), default, BindingMode.TwoWay);
        public static BindableProperty textProperty = 
            BindableProperty.Create(nameof(text), typeof(string), typeof(LabelEntry), default, BindingMode.TwoWay);

        public LabelEntry()
        {
            InitializeComponent();
            _Label.BindingContext = this;
            _Label.SetBinding(Label.TextProperty,nameof(label));
            _Entry.BindingContext = this;
            _Entry.SetBinding(Entry.TextProperty,nameof(text));
            _Label1.BindingContext = this;
            _Label1.SetBinding(Label.TextProperty,nameof(text));

        }
        public string label { get => (string)GetValue(labelProperty); set => SetValue(labelProperty, value); }
        public string text { get => (string)GetValue(textProperty); set => SetValue(textProperty, value); }

    }
}