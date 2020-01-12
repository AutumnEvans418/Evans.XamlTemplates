
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace MSBuild
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test: ContentView
    {
        public static BindableProperty HelloProperty = 
            BindableProperty.Create(nameof(Hello), typeof(string), typeof(Test), default, BindingMode.TwoWay);

        public Test()
        {
            InitializeComponent();
            _Label.BindingContext = this;
_Label.SetBinding(Xamarin.Forms.Label.TextProperty,nameof(Hello));

        }
        public string Hello { get => (string)GetValue(HelloProperty); set => SetValue(HelloProperty, value); }

    }
}