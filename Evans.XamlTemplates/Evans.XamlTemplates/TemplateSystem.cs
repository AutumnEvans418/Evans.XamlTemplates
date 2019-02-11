using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using VisualElement = Xamarin.Forms.PlatformConfiguration.iOSSpecific.VisualElement;

namespace Evans.XamlTemplates
{
    public class BindingProperty
    {
        public string Name { get; set; }
    }

    public class BindingPropertyList : List<BindingProperty>
    {
        public BindingPropertyList(IEnumerable<BindingProperty> properties) : base(properties)
        {

        }
        public BindingPropertyList()
        {

        }
        public static implicit operator BindingPropertyList(string value)
        {
            if (value.Contains(","))
            {
                var list = value.Split(',').Select(p => new BindingProperty() { Name = p });
                return new BindingPropertyList(list);
            }
            else
            {
                return new BindingPropertyList() { new BindingProperty() { Name = value } };
            }
        }
    }
    public static class TemplateSystem
    {
        public static readonly BindableProperty TemplatePropertiesProperty =
            BindableProperty.CreateAttached("TemplateProperties",
                typeof(BindingPropertyList), typeof(TemplateSystem),
                new BindingPropertyList(), BindingMode.Default, null, BindingPropertiesChanged);

      
        public static BindingPropertyList GetTemplateProperties(BindableObject view)
        {
            return (BindingPropertyList)view.GetValue(TemplatePropertiesProperty);
        }


        public static void SetTemplateProperties(BindableObject view, BindingPropertyList value)
        {
            view.SetValue(TemplatePropertiesProperty, value);
        }
        private static void BindingPropertiesChanged(BindableObject bindable, object oldvalue, object newvalue)
        {

        }
    }
}