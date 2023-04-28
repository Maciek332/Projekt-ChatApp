using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T storage, T value, string propertyName, Action validator = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            validator?.Invoke();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
