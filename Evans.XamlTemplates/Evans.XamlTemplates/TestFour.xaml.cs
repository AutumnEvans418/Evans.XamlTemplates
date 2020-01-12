
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
    public partial class TestFour: ContentView
    {
        public static BindableProperty hiProperty = 
            BindableProperty.Create(nameof(hi), typeof(string), typeof(TestFour), default, BindingMode.TwoWay);

        public TestFour()
        {
            InitializeComponent();
            
        }
        public string hi { get => (string)GetValue(hiProperty); set => SetValue(hiProperty, value); }

    }
}