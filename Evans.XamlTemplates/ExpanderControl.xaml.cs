
//WARNING! THIS CODE IS GENERATED BY XAML TEMPLATES.  DO NOT CHANGE
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
    public partial class ExpanderControl: ContentView
    {
        public static BindableProperty TextProperty = 
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ExpanderControl), default, BindingMode.TwoWay);
        public static BindableProperty ExpandedProperty = 
            BindableProperty.Create(nameof(Expanded), typeof(bool), typeof(ExpanderControl), default, BindingMode.TwoWay);
        public static BindableProperty ExpandContentProperty = 
            BindableProperty.Create(nameof(ExpandContent), typeof(object), typeof(ExpanderControl), default, BindingMode.TwoWay);

        public ExpanderControl()
        {
            InitializeComponent();
            _Label.BindingContext = this;
            _Label.SetBinding(Label.TextProperty,nameof(Text));
            _Switch.BindingContext = this;
            _Switch.SetBinding(Switch.IsToggledProperty,nameof(Expanded));
            _ContentView.BindingContext = this;
            _ContentView.SetBinding(ContentView.ContentProperty,nameof(ExpandContent));
            _ContentView.SetBinding(ContentView.IsVisibleProperty,nameof(Expanded));
            Text = "Default Label";
            Expanded = true;

        }
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        public bool Expanded { get => (bool)GetValue(ExpandedProperty); set => SetValue(ExpandedProperty, value); }
        public object ExpandContent { get => (object)GetValue(ExpandContentProperty); set => SetValue(ExpandContentProperty, value); }

    }
}