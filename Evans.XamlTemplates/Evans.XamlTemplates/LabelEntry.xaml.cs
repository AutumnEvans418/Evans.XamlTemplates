using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Evans.XamlTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LabelEntry : ContentView
    {
        public static BindableProperty LabelProperty = 
            BindableProperty.Create(nameof(Label), typeof(string), typeof(LabelEntry), default, BindingMode.TwoWay, propertyChanged:LabelPropertyChanged);

        private static void LabelPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((LabelEntry) bindable)._Label.Text = (string)newvalue;
        }

        public static BindableProperty EntryProperty = 
            BindableProperty.Create(nameof(Label), typeof(string), typeof(LabelEntry), default, BindingMode.TwoWay, propertyChanged: EntryPropertyChanged);

        private static void EntryPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((LabelEntry)bindable)._Entry.Text = (string)newvalue;
        }

        public LabelEntry()
        {
            InitializeComponent();
        }

        public object Label
        {
            get => GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public object Entry
        {
            get => GetValue(EntryProperty);
            set => SetValue(EntryProperty, value);
        } 
    }
}