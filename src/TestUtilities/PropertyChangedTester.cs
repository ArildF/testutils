using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TestUtilities
{
    public class PropertyChangedTester<T>
    {
        private readonly T _obj;
        private readonly List<string> _raisedEvents;

        public PropertyChangedTester(T obj)
        {
            _obj = obj;
            _raisedEvents = new List<string>();

            INotifyPropertyChanged ipc = obj as INotifyPropertyChanged;
            if (ipc != null)
            {
                ipc.PropertyChanged += PropertyChangedHandler;
            }
        }

        void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            _raisedEvents.Add(e.PropertyName);
        }

        public bool PropertyChanged<TReturn>(Expression<Func<T, TReturn>> expression)
        {
            string propName = PropertyNameFromExpression(expression);
            return _raisedEvents.Contains(propName);

        }

        private static string PropertyNameFromExpression<TReturn>(Expression<Func<T, TReturn>> expression)
        {
            MemberExpression me = expression.Body as MemberExpression;
            if (me == null)
            {
                throw new InvalidOperationException("Expression must be a property access.");
            }

            return me.Member.Name;
        }
    }
}
