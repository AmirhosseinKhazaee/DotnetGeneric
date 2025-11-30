using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XGeneric.Attributes;
using XGeneric.Models;

namespace XGeneric.Extensions
{
    public static class AttributeExtension
    {
        public static bool HasKey<T>(this T source)
        where T : BaseModel
        {
            var result = false;
            var keyMetadata = source.GetKeyMetadata();
            var count = keyMetadata!.Count;
            if (count > 0)
            {
                result = true;
            }
            return result;
        }
        public static bool HasOneKey<T>(this T source)
        where T : BaseModel
        {
            var result = false;
            var keyMetadata = source.GetKeyMetadata();
            if (keyMetadata!.Count == 1)
            {
                result = true;
            }
            return result;

        }
        public static XKeyAttribute? GetKeyMetadata<T>(this T source)
        where T : BaseModel
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
            var count = typeof(T).GetProperties()
            .Count(p => Attribute.IsDefined(p, typeof(XKeyAttribute)));

            // Attach metadata
            attr.KeyFieldName = prop.Name;
            attr.KeyFieldType = prop.PropertyType;
            attr.Count = count;
            attr.KeyFieldValue = prop.GetValue(source);
            return attr;
        }
        public static bool IsXBaseModel<T>(this T source)
            where T : BaseModel
        {
            var isBaseModel = Attribute.IsDefined(typeof(T), typeof(XBaseModelAttribute))
                              && source.HasOneKey();

            if (!isBaseModel)
                return false;

            // Auto-set base fields
            source.CreatedAt = DateTime.UtcNow;
            source.UpdatedAt = DateTime.UtcNow;
            source.DeletedAt = null;
            source.SoftDeleted = false;

            return true;
        }


    }
}