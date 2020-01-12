using System;
using System.IO;
using System.Reflection;
using Evans.XamlTemplates;
using Evans.XamlTemplates.Generator;
using Microsoft.Build.Framework;

namespace XamlTemplates.MSBuild
{
    public class MyExternalTask : Microsoft.Build.Utilities.Task
    {
        public string MyParameter { get; set; }
        public string Filter { get; set; } = "*.taml";

        public string Namespace = 
            Assembly.GetEntryAssembly()?.GetName().Name ?? "Template";
        public override bool Execute()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), Filter);
            var parser = new TamlParser();
            var ast = new TamlAst();
            var gen = new Generator();
            gen.Namespace = Namespace;
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                var tokens = parser.GetTokens(content);
                var t = ast.Evaluate(tokens);

                var result = gen.Generate(t);

                foreach (var generatedType in result)
                {
                    var x = generatedType.Xaml;
                    var c = generatedType.CSharp;

                    File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(),x.FileName), x.Content);
                    File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(),c.FileName), c.Content);
                }
            }
            return true;
        }
    }
}
