using System.Collections.Generic;

namespace Evans.XamlTemplates.Generator
{
    public class Generator
    {
        public IEnumerable<GeneratedType> Generate(Program program)
        {
            var nameGen = new NameGenerator();
            var xaml = new GenerateXaml(nameGen);
            var csharp = new GenerateCSharp(nameGen);
            foreach (var programTemplate in program.Templates)
            {
                yield return new GeneratedType(xaml.Generate(programTemplate),csharp.Generate(programTemplate));
            }
        }
    }
}