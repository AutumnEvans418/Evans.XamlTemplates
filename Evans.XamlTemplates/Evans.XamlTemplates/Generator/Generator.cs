using System;
using System.Collections.Generic;

namespace Evans.XamlTemplates.Generator
{
    public class Generator
    {
        public IEnumerable<GeneratedType> Generate(Program program)
        {
            var xaml = new GenerateXaml();
            var csharp = new GenerateCSharp();
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