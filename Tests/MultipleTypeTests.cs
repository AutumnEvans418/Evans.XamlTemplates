using System;
using Evans.XamlTemplates;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MultipleTypeTests
    {
        string code = @"
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

        readonly Templator templator = new Templator();
        [Test]
        public void EmbbedTypes_Should_Pass()
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