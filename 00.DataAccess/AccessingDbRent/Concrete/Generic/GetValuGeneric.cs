using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Concrete.Generic
{
    internal class GetValuGeneric
    {
        public object GetPropertyValue<T>(T item, string propertyName)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
            return propertyInfo != null ? propertyInfo.GetValue(item, null) : null;
        }
    }
}
