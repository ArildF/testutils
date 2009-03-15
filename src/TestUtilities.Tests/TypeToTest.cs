using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TestUtilities.Tests
{
    class TypeToTest : INotifyPropertyChanged
    {
        private int _someProperty;
        public event PropertyChangedEventHandler PropertyChanged;

        public int SomeProperty
        {
            get {
                return _someProperty;
            }
            set {
                _someProperty = value;
                RaisePropertyChanged("SomeProperty");

            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object SomeMethod()
        {
            return null;
        }
    }
}
