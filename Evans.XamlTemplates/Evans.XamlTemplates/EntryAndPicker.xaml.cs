
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
        public static BindableProperty DataProperty = 
            BindableProperty.Create(nameof(Data), typeof(IEnumerable<string>), typeof(EntryAndPicker), default, BindingMode.TwoWay);
        public static BindableProperty SelectedItemProperty = 
            BindableProperty.Create(nameof(SelectedItem), typeof(string), typeof(EntryAndPicker), default, BindingMode.TwoWay);

        public EntryAndPicker()
        {
            InitializeComponent();
            _Label.BindingContext = this;
            _Label.SetBinding(Xamarin.Forms.Label.TextProperty,nameof(Label));
            _Entry.BindingContext = this;
            _Entry.SetBinding(Xamarin.Forms.Entry.TextProperty,nameof(Text));
            _Label1.BindingContext = this;
            _Label1.SetBinding(Xamarin.Forms.Label.TextProperty,nameof(Text));
            _Picker.BindingContext = this;
            _Picker.SetBinding(Xamarin.Forms.Picker.ItemsSourceProperty,nameof(Data));
            _Picker.SetBinding(Xamarin.Forms.Picker.SelectedItemProperty,nameof(SelectedItem));
            _Label2.BindingContext = this;
            _Label2.SetBinding(Xamarin.Forms.Label.TextProperty,nameof(SelectedItem));

        }
        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        public IEnumerable<string> Data { get => (IEnumerable<string>)GetValue(DataProperty); set => SetValue(DataProperty, value); }
        public string SelectedItem { get => (string)GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

    }
}