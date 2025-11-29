using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XGeneric.Attributes;

namespace XGeneric.Extensions
{
    public class AttributeExtension
    {
        public static XKeyAttribute? GetKeyMetadata<T>()
        {
            var prop = typeof(T)
                .GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(XKeyAttribute)));

            if (prop == null)
            {
                // Return null if no key found
                return null;
            }

            var attr = prop.GetCustomAttribute<XKeyAttribute>();
            if (attr == null)
                return null;

            // Attach metadata
            attr.KeyFieldName = prop.Name;
            attr.KeyFieldType = prop.PropertyType;

            return attr;
        }
    }
}