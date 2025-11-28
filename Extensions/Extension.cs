using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using XGeneric.Attributes;
using XGeneric.Models;

namespace XGeneric.Extensions
{
    public static class Extension
    {


        public static bool IsExists<T>(this IDictionary<Guid, T> repo, Guid id)
        {
            if (id == Guid.Empty)
                return false;

            if (repo == null || repo.Count == 0)
                return false;

            return repo.ContainsKey(id);
        }

        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsDefaultGuid(this Guid id)
        {
            return id == Guid.Empty;
        }


        public static string GetControllerName(this ControllerContext source)
        {
            //
            var result = source.ActionDescriptor.ControllerName;

            //
            return result;
        }



        public static XKeyAttribute GetKeyMetadata<T>()
        {
            var prop = typeof(T)
                .GetProperties()
                .FirstOrDefault(p =>
                    Attribute.IsDefined(p, typeof(XKeyAttribute)));

            if (prop == null)
                throw new Exception($"No property with [XKey] found on type {typeof(T).Name}");

            var attr = (XKeyAttribute)prop.GetCustomAttribute(typeof(XKeyAttribute));

            // Attach metadata
            attr.KeyFieldName = prop.Name;     // ✔ works
            attr.KeyFieldType = prop.PropertyType;  // ✔ works


            return attr;
        }
    }

}



