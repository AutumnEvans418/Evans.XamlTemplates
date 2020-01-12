using System;
using System.Collections.Generic;
using System.Reflection;

namespace Evans.XamlTemplates.Generator
{
    public class Generator
    {
        public string Namespace { get; set; } = Assembly.GetCallingAssembly().GetName().Name;
        public IEnumerable<GeneratedType> Generate(Program program)
        {
            var xaml = new GenerateXaml();
            xaml.AssemblyName = Namespace;
            var csharp = new GenerateCSharp();
            csharp.AssemblyName = Namespace;
            foreach (var programTemplate in program.Templates)
            {
                if(programTemplate.Body == null) throw new ArgumentNullException(nameof(programTemplate.Body));
                var nameGen = new NameGenerator(programTemplate.Body.Controls);

                var x = xaml.Generate(programTemplate);
                var c = csharp.Generate(programTemplate, nameGen);
                yield return new GeneratedType(x,c);
            }
        }
    }
}