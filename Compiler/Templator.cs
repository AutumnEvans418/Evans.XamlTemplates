using System.Collections.Generic;
using Evans.XamlTemplates.Generator;

namespace Evans.XamlTemplates
{
    public class Templator
    {
        TamlParser parser = new TamlParser();
        TamlAst ast = new TamlAst();
        Generator.Generator gen = new Generator.Generator();
        public IEnumerable<GeneratedType> Generate(string code, string Namespace)
        {
            gen.Namespace = Namespace;
            var tokens = parser.GetTokens(code);
            var t = ast.Evaluate(tokens);
            return gen.Generate(t);
        }
    }
}