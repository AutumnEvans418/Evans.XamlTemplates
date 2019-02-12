using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Evans.XamlTemplates
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = this;

            InitializeComponent();
        }

        public string Value { get; set; } = "test";
        public string Value1 { get; set; } = "test1";
    }
}
