using System;
using System.Linq;

namespace Evans.XamlTemplates
{
    public class SandboxViewModel : BindableBase
    {
        public string Code
        {
            get => _code;
            set => SetProperty(ref _code, value, CodeChanged);
        }

        public string Xaml
        {
            get => _xaml;
            set => SetProperty(ref _xaml, value);
        }

        public string CSharp
        {
            get => _cSharp;
            set => SetProperty(ref _cSharp, value);
        }

        public string Error
        {
            get => _error;
            set => SetProperty(ref _error,value);
        }

        readonly Templator _templator = new Templator();
        private string _code = Examples.Basic;
        private string _xaml = "";
        private string _cSharp = "";
        private string _error = "";

        public SandboxViewModel()
        {
            CodeChanged();
        }
        public void CodeChanged()
        {
            try
            {
                var result = _templator.Generate(Code, "Templates").FirstOrDefault();

                if (result != null)
                {
                    Xaml = result.Xaml.Content;
                    CSharp = result.CSharp.Content;
                    Error = "";
                }
            }
            catch (CompileException e)
            {
                Error = e.ToString();
            }
            catch (Exception e)
            {
                Error = e.ToString();
                throw;
            }
        }
    }
}