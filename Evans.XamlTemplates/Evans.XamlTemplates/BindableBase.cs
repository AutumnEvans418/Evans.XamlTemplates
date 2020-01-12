using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Evans.XamlTemplates
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T prop, T value, Action? action = null, [CallerMemberName] string? propertyName = null)
        {
            if (prop?.Equals(value) != true)
            {
                prop = value;
                action?.Invoke();
                OnPropertyChanged(propertyName);
            }
        }

    }
}