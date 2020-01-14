using System;
using Evans.XamlTemplates;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MultipleTypeTests
    {
       public const string code = @"
@LabelEntry(string label,string Text)
{
	<StackLayout>
		<Label Text=""@label""/>
		<Entry Text=""@Text""/>
	</StackLayout>
}

@Header(string Text)
{
    <Label Text=""@Text"" FontSize=""Large""/>
}

@Section(string Header, object Content)
{
    <StackLayout xmlns:local=""clr-namespace:Example"">
        <local:Header Text=""@Header""/>
        <ContentView Content=""@Content""/>
    </StackLayout>
}
";

       public const string codeWithoutXmlns = @"
@Header(string Text)
{
    <Label Text=""@Text"" FontSize=""Large""/>
}
@Section(string Header, object Content)
{
    <StackLayout>
        <local:Header Text=""@Header""/>
        <ContentView Content=""@Content""/>
    </StackLayout>
}
";
        public const string codeWithoutXmlnsAndLocal = @"
@Header(string Text)
{
    <Label Text=""@Text"" FontSize=""Large""/>
}
@Section(string HeaderText, object Content)
{
    <StackLayout>
        <Header Text=""@HeaderText""/>
        <ContentView Content=""@Content""/>
    </StackLayout>
}
";

        readonly Templator templator = new Templator();
        [TestCase(code)]
        [TestCase(codeWithoutXmlns)]
        [TestCase(codeWithoutXmlnsAndLocal)]
        public void EmbbedTypes_Should_Pass(string code)
        {
            var result = templator.Generate(code, "Example");

            foreach (var generatedType in result)
            {
                Console.WriteLine(generatedType.Xaml.Content);
                Console.WriteLine(generatedType.CSharp.Content);
            }
        }

    }
}