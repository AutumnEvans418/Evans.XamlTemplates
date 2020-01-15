using System;
using System.Linq;
using System.Text.RegularExpressions;
using Evans.XamlTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ExpectedOutputTests
    {

        public const string expectedCodeWithoutXmlns = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ContentView xmlns=""http://xamarin.com/schemas/2014/forms"" 
             xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
             xmlns:d=""http://xamarin.com/schemas/2014/forms/design""
             xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
             xmlns:local=""clr-namespace:Test""
             mc:Ignorable=""d""
             x:Class=""Test.Header"">
  <ContentView.Content>
      <Label FontSize=""Large"" x:Name=""_Label"" xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml"" />
    </ContentView.Content>
</ContentView>";

        public const string SetContent = @"
@test()
{
    <syncfusion:SfDataGrid xmlns:syncfusion=""clr-namespace:Syncfusion.SfDataGrid.XForms;assembly=Syncfusion.SfDataGrid.XForms"">
        <syncfusion:SfDataGrid.AutoGenerateColumns>True</syncfusion:SfDataGrid.AutoGenerateColumns>
    </syncfusion:SfDataGrid>
}";

        public const string ThirdPartyControl = @"
@DataGridSection(string Header, IEnumerable<object> Data)
{
    <syncfusion:SfDataGrid 
        ItemsSource=""@Data"" 
        AutoGenerateColumns=""True"" 
        xmlns:syncfusion=""clr-namespace:Syncfusion.SfDataGrid.XForms;assembly=Syncfusion.SfDataGrid.XForms""
        />
}";


        public const string ThirdPartyControlOutput = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ContentView xmlns=""http://xamarin.com/schemas/2014/forms""
             xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
             xmlns:d=""http://xamarin.com/schemas/2014/forms/design""
             xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
             xmlns:local=""clr-namespace:Test""
             mc:Ignorable=""d""
             x:Class=""Test.DataGridSection"">
  <ContentView.Content>
      <syncfusion:SfDataGrid AutoGenerateColumns=""True"" xmlns:syncfusion=""clr-namespace:Syncfusion.SfDataGrid.XForms;assembly=Syncfusion.SfDataGrid.XForms"" x:Name=""_SfDataGrid"" xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml"" />
    </ContentView.Content>
</ContentView>";


        public const string Syncfusion = @"@DataGridSection(string HeaderText, IEnumerable<object> Data)
{
    <StackLayout xmlns:local=""clr-namespace:Evans.XamlTemplates"" xmlns:syncfusion=""clr-namespace:Syncfusion.SfDataGrid.XForms;assembly=Syncfusion.SfDataGrid.XForms"">
        <local:Header Text=""@HeaderText"" />
        <syncfusion:SfDataGrid ItemsSource=""@Data"" AutoGenerateColumns=""True""/>
    </StackLayout>
}";

        public const string SyncfusionOutputXaml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ContentView xmlns=""http://xamarin.com/schemas/2014/forms"" 
             xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
             xmlns:d=""http://xamarin.com/schemas/2014/forms/design""
             xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
             xmlns:local=""clr-namespace:Test""
             mc:Ignorable=""d""
             x:Class=""Evans.XamlTemplates.DataGridSection"">
  <ContentView.Content>
      <StackLayout xmlns:local=""clr-namespace:Evans.XamlTemplates"" xmlns:syncfusion=""clr-namespace:Syncfusion.SfDataGrid.XForms;assembly=Syncfusion.SfDataGrid.XForms"">
  <local:Header x:Name=""_Header"" xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml"" />
  <syncfusion:SfDataGrid AutoGenerateColumns=""True"" x:Name=""_SfDataGrid"" xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml"" />
</StackLayout>
    </ContentView.Content>
</ContentView>";

        public const string SyncfusionOutputCSharp = @"
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
    public partial class DataGridSection: ContentView
    {
        public static BindableProperty HeaderTextProperty = 
            BindableProperty.Create(nameof(HeaderText), typeof(string), typeof(DataGridSection), default, BindingMode.TwoWay);
        public static BindableProperty DataProperty = 
            BindableProperty.Create(nameof(Data), typeof(IEnumerable<object>), typeof(DataGridSection), default, BindingMode.TwoWay);

        public DataGridSection()
        {
            InitializeComponent();
            _Header.BindingContext = this;
            _Header.SetBinding(local:Header.TextProperty,nameof(HeaderText));
            _SfDataGrid.BindingContext = this;
            _SfDataGrid.SetBinding(syncfusion:SfDataGrid.ItemsSourceProperty,nameof(Data));

        }
        public string HeaderText { get => (string)GetValue(HeaderTextProperty); set => SetValue(HeaderTextProperty, value); }
        public IEnumerable<object> Data { get => (IEnumerable<object>)GetValue(DataProperty); set => SetValue(DataProperty, value); }

    }
}";



        [TestCase(MultipleTypeTests.codeWithoutXmlns, expectedCodeWithoutXmlns)]
        [TestCase(ThirdPartyControl, ThirdPartyControlOutput)]
        [TestCase(SetContent, "")]
        [TestCase(Syncfusion, SyncfusionOutputXaml)]
        public void Input_Should_Expect_Xaml(string input, string outputXaml)
        {
            var templator = new Templator();

            var result = templator.Generate(input, "Test").First().Xaml.Content;
           var result2 = Regex.Replace(result, @"\s+", "");
            Console.WriteLine(result);
            result2.Should().Be(Regex.Replace(outputXaml, @"\s+", ""));
        }
        [TestCase(Syncfusion, SyncfusionOutputCSharp)]
        public void Input_Should_Expect_CSharp(string input, string outputCSharp)
        {
            var templator = new Templator();

            var result = templator.Generate(input, "Test").First().CSharp.Content;
            var result2 = Regex.Replace(result, @"\s+", "");
            Console.WriteLine(result);
            result2.Should().Be(Regex.Replace(outputCSharp, @"\s+", ""));
        }
    }
}