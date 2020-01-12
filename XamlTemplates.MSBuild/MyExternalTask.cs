using System;
using Microsoft.Build.Framework;

namespace XamlTemplates.MSBuild
{
    public class MyExternalTask : Microsoft.Build.Utilities.Task
    {
        public string MyParameter { get; set; }

        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High,MyParameter);
            Console.WriteLine("Hello from the MyAssembly: " + MyParameter);
            return true;
        }
    }
}
