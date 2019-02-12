using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Xamarin.Forms;
using VisualElement = Xamarin.Forms.PlatformConfiguration.iOSSpecific.VisualElement;

namespace Evans.XamlTemplates
{



    public class Field
    {
        public Field(string name, Type type, object value)
        {
            this.FieldName = name;
            this.FieldType = type;
            Value = value;
        }

        public object Value;
        public string FieldName;

        public Type FieldType;
    }
    public class DynamicClass : DynamicObject
    {
        private Dictionary<string, KeyValuePair<Type, object>> _fields;

        public DynamicClass(List<Field> fields)
        {
            _fields = new Dictionary<string, KeyValuePair<Type, object>>();
            fields.ForEach(x => _fields.Add(x.FieldName,
                new KeyValuePair<Type, object>(x.FieldType, x.Value)));
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            
            if (_fields.ContainsKey(binder.Name))
            {
                var type = _fields[binder.Name].Key;
                if (value.GetType() == type)
                {
                    _fields[binder.Name] = new KeyValuePair<Type, object>(type, value);
                    return true;
                }
                else throw new Exception("Value " + value + " is not of type " + type.Name);
            }
            return false;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _fields[binder.Name].Value;
            return true;
        }
    }

    public class BindingProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }

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

        static Dictionary<BindableObject, BindingPropertyList> TemplateObjects = new Dictionary<BindableObject, BindingPropertyList>();
        private static void BindingPropertiesChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            bindable.BindingContextChanged +=
                (sender, args) =>
                {
                    if(TemplateObjects.ContainsKey((BindableObject)sender))
                    SetBindingsForTemplate((BindableObject) sender, TemplateObjects[(BindableObject) sender]);
                };
            SetBindingsForTemplate(bindable, newvalue);
        }

        private static void SetBindingsForTemplate(BindableObject bindable, object newvalue)
        {
            if (newvalue is BindingPropertyList list && bindable is ContentView contentView)
            {
                if (TemplateObjects.ContainsKey(bindable) != true)
                {
                    TemplateObjects.Add(bindable, list);
                }

                if (list.All(p => p.Value != null) && contentView.BindingContext != null)
                {
                    var contextList = new List<Field>()
                    {
                        new Field("Test", typeof(string), "Test worked!"), new Field("Test1", typeof(string), "Test 2 Worked!")
                    };

                    //foreach (var bindingProperty in list)
                    //{
                    //    contextList.Add(new Field(bindingProperty.Name, bindingProperty.Value.GetType(), bindingProperty.Value));
                    //}

                    dynamic context = new DynamicClass(contextList);
                   contentView.Content.BindingContext = context; //new {Test = list.First().Value, Test1 = "test2"};
                }
            }
        }
    }
}