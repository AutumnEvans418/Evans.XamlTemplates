using Evans.XamlTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ParserTests
    {
        string code = @"
@LabelEntry(string Label,string Text)
{
	<StackLayout>
		<Label Text=""@Label""/>
		<Entry Text=""@Text""/>
	</StackLayout>
}";
        [Test]
        public void GetTokens()
        {
            var parser = new TamlParser();

            var tokens = parser.GetTokens(code);

            tokens.Should().NotBeEmpty();
        }


        [Test]
        public void GenerateTree()
        {
            var parser = new TamlParser();

            var tamlAst = new TamlAst();

            var tokens = parser.GetTokens(code);

            var program = tamlAst.Evaluate(tokens);
        }


    }

    
}