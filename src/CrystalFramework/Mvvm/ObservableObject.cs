using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CrystalFramework.Mvvm
{
    class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public T GetValue<T>(string propertyName, T defaultValue = default)
        {
            if (_properties.ContainsKey(propertyName))
                return (T)_properties[propertyName];
            else
                return defaultValue;
        }

        public void SetValue<T>(string propertyName, T newValue)
        {
            if (_properties.ContainsKey(propertyName))
            {
                var currentValue = (T)_properties[propertyName];

                if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                    return;

                _properties[propertyName] = newValue;
                OnPropertyChanged(propertyName);
            }
            else
            {
                _properties.Add(propertyName, newValue);
                OnPropertyChanged(propertyName);
            }
        }
    }
}
