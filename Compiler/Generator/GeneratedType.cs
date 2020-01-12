namespace Evans.XamlTemplates.Generator
{
    public class GeneratedType
    {
        public GeneratedType(GeneratedFile xaml, GeneratedFile cSharp)
        {
            Xaml = xaml;
            CSharp = cSharp;
        }

        public GeneratedFile Xaml { get; set; }
        public GeneratedFile CSharp { get; set; }
    }
}