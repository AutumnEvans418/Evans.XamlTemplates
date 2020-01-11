using Evans.XamlTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void GetTokens()
        {
            var code = @"
@LabelEntry(string Label,string Text)
{
	<StackLayout>
		<Label Text=""@Label""/>
		<Entry Text=""@Text""/>
	</StackLayout>
}";

            var parser = new TamlParser();

            var tokens = parser.GetTokens(code);

            tokens.Should().Contain(p => p.TokenType == TokenType.Xml);

        }
    }
}