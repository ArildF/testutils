using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TestUtilities.Tests
{
    class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    class TypeToTest : PropertyChangedBase
    {
        private int _someProperty;
        private string _someOtherProperty;
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

        public string SomeOtherProperty
        {
            get {
                return _someOtherProperty;
            }
            set {
                _someOtherProperty = value;
                RaisePropertyChanged("SomeOtherProperty");

            }
        }

       

        public object SomeMethod()
        {
            return null;
        }
    }

    class TypeThatDoesntRaise : PropertyChangedBase
    {
        public string Prop { get; set; }
    }

    class TypeWithProtectedProperty : PropertyChangedBase
    {
        public string Prop { get; protected set; }
    }
}
