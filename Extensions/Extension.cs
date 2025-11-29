using Microsoft.AspNetCore.Mvc;

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

    }

}



