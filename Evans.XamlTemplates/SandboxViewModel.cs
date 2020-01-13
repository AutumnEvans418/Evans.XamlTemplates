using System;
using System.Linq;

namespace Evans.XamlTemplates
{
    public class SandboxViewModel : BindableBase
    {
        public string Entry1
        {
            get => _entry1;
            set => SetProperty(ref _entry1,value);
        }

        public string Entry2
        {
            get => _entry2;
            set => SetProperty(ref _entry2,value);
        }

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
        private string _entry1 = "";
        private string _entry2 = "";

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
                else
                {
                    Xaml = "";
                    CSharp = "";
                    Error = "";
                }
            }
            catch (CompileException e)
            {
                Error = e.ToString();
                Xaml = "";
                CSharp = "";
            }
            catch (Exception e)
            {
                Error = e.ToString();
                Xaml = "";
                CSharp = "";
                throw;
            }
        }
    }
}