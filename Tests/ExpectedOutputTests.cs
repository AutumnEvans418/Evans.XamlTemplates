using System;
using System.Linq;
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


        [TestCase(MultipleTypeTests.codeWithoutXmlns, expectedCodeWithoutXmlns)]
        public void Input_Should_Expect_Xaml(string input, string outputXaml)
        {
            var templator = new Templator();

           var result = templator.Generate(input, "Test").First().Xaml.Content;
               
           Console.WriteLine(result);
               result.Should().Be(outputXaml);
        }
    }
}