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
        private string _value1 = "test1";

        public MainPage()
        {
            BindingContext = this;

            InitializeComponent();
        }

        public string Value { get; set; } = "test";
        public IEnumerable<string> Data { get; set; } = new List<string>(){"test", "test2", "test3", "DESTROY"};
        public string Value1
        {
            get => _value1;
            set
            {
                _value1 = value;
                Console.WriteLine(value);
                OnPropertyChanged(nameof(Value1));
            }
        }
    }
}
