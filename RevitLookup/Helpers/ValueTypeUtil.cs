

namespace RevitLookupWpf.Helpers
{
    public static class ValueTypeUtil
    {
        /// <summary>
        /// Determine if  a type is value type of string type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValueTypeOrString(this Type type)
        {
            if (type == null)
                return false;

            return type.IsValueType || type == typeof(string);
        }
    }
}
