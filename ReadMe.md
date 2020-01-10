# Xaml Templates
Xaml templates is a templating engine for xamarin forms that allows you to build templates with ease without having to make custom controls.

**Turn 40+ lines of code to just 8**

## Example
Below is an example of a template in a file called template.taml

```csharp
@LabelEntry(string Label,string Text)
{
	<StackLayout>
		<Label Text="@Label"/>
		<Entry Text="@Text"/>
	</StackLayout>
}
```

This will be generated to the following c# and xaml file

LabelEntry.xaml
```xml
<?xml version="1.0" encoding="UTF-8"?>
<ContentView xmlns="http://xamarin.com/schemas/2014/forms" 
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             x:Class="Evans.XamlTemplates.LabelEntry">
  <ContentView.Content>
      <StackLayout>
          <Label x:Name="_Label"/>
          <Entry x:Name="_Entry"/>
      </StackLayout>
    </ContentView.Content>
</ContentView>
```

LabelEntry.xaml.cs
```csharp
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Evans.XamlTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LabelEntry : ContentView
    {
        public static BindableProperty LabelProperty = 
            BindableProperty.Create(nameof(Label), typeof(object), typeof(LabelEntry), default, BindingMode.TwoWay, propertyChanged:LabelPropertyChanged);

        private static void LabelPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((LabelEntry) bindable)._Label.Text = (string)newvalue;
        }

        public static BindableProperty EntryProperty = 
            BindableProperty.Create(nameof(Label), typeof(object), typeof(LabelEntry), default, BindingMode.TwoWay, propertyChanged: EntryPropertyChanged);

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
```

Notice how much code it takes to just make a template?  There needs to be a simpler solution

# Analysis

## What needs to be known?

- ClassName
- Parameters (Comma Seperated)
  - Name of Parameter
  - Name with Underscore
  - Type of Parameter
- 
