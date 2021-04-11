using System;

namespace WebAPI.Extensions
{
    public static class StringExtensions
    {
        public static string SafeTrim(this string value) => string.IsNullOrEmpty(value) ? null : value.Trim();
        
        public static string ToStringName<TEnum>(this TEnum key) => Enum.GetName(typeof(TEnum), key);
    }
}