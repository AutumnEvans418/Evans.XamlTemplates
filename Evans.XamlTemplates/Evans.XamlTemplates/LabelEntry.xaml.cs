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
}