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

        public int Count { get; set; }
        public object? KeyFieldValue { get; set; }

        public XKeyAttribute()
        {
        }

        public override string ToString()
        {
            //
            var result = string.Empty;

            //
            result += $"{typeof(XKeyAttribute)} \n" +
                $"KeyFieldName: {KeyFieldName} \n" +
                $"KeyFieldType: {KeyFieldType} \n" +
                $"Count: {Count} \n" +
                $"KeyFieldValue: {KeyFieldValue} \n";

            //
            return result;
        }
    }
}