using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using XGeneric.Extensions;

namespace XGeneric.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XKeyAttribute : Attribute
    {
        public string? KeyFieldName { get; set; }
        public Type? KeyFieldType { get; set; }

        public object KeyFieldValue {get; set;}
        public XKeyAttribute()
        {
        }


    }
}