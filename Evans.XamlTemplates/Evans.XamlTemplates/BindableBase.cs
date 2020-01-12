using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Evans.XamlTemplates.Annotations;

namespace Evans.XamlTemplates
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T prop, T value, Action? action = null)
        {
            if (prop?.Equals(value) != true)
            {
                prop = value;
                action?.Invoke();
            }
        }

    }
}