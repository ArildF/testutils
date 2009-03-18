using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestUtilities
{
    static class TypeExtensions
    {
        public static object DefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        } 
    }
}
