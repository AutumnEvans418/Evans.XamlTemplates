using System;
using System.Linq;
using Evans.XamlTemplates;
using Evans.XamlTemplates.Generator;
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

        [Test]
        public void xProp_Should_GenerateTree()
        {
            var parser = new TamlParser();

            var tamlAst = new TamlAst();

            string code = @"
@LabelEntry(string Label,string Text)
{
    <Label 
        xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml"" 
        x:Name=""test""/>
}";

            var tokens = parser.GetTokens(code);

            var program = tamlAst.Evaluate(tokens);

            program.Templates.First().Body.Controls.First().ControlProperties.Last().Name.Should().Be("x:Name");

        }



        [Test]
        public void GenerateFiles()
        {
            var parser = new TamlParser();

            var tamlAst = new TamlAst();

            var gen = new Generator();
            var tokens = parser.GetTokens(code);

            var program = tamlAst.Evaluate(tokens);

            var result = gen.Generate(program);

            result.First().Xaml.FileName.Should().Be("LabelEntry.xaml");
            result.First().CSharp.FileName.Should().Be("LabelEntry.xaml.cs");

            
            Console.WriteLine(result.First().Xaml.Content);
            Console.WriteLine(result.First().CSharp.Content);
        }

    }

    
}