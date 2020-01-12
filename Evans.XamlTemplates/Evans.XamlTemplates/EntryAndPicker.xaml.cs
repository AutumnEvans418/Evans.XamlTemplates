
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
    public partial class EntryAndPicker: ContentView
    {
        public static BindableProperty LabelProperty = 
            BindableProperty.Create(nameof(Label), typeof(string), typeof(EntryAndPicker), default, BindingMode.TwoWay);
public static BindableProperty TextProperty = 
            BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryAndPicker), default, BindingMode.TwoWay);
public static BindableProperty dataProperty = 
            BindableProperty.Create(nameof(data), typeof(IEnumerable<string>), typeof(EntryAndPicker), default, BindingMode.TwoWay);
public static BindableProperty selectedItemProperty = 
            BindableProperty.Create(nameof(selectedItem), typeof(string), typeof(EntryAndPicker), default, BindingMode.TwoWay);

        public EntryAndPicker()
        {
            InitializeComponent();
            _Label.BindingContext = this;
_Label.SetBinding(Xamarin.Forms.Label.TextProperty,nameof(Label));
_Entry.BindingContext = this;
_Entry.SetBinding(Xamarin.Forms.Entry.TextProperty,nameof(Text));
_Picker.BindingContext = this;
_Picker.SetBinding(Xamarin.Forms.Picker.ItemsSourceProperty,nameof(data));
_Picker.SetBinding(Xamarin.Forms.Picker.SelectedItemProperty,nameof(selectedItem));
_Label2.BindingContext = this;
_Label2.SetBinding(Xamarin.Forms.Label.TextProperty,nameof(selectedItem));

        }
        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
public IEnumerable<string> data { get => (IEnumerable<string>)GetValue(dataProperty); set => SetValue(dataProperty, value); }
public string selectedItem { get => (string)GetValue(selectedItemProperty); set => SetValue(selectedItemProperty, value); }

    }
}