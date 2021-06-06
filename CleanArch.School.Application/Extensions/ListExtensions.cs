namespace CleanArch.School.Application.Extensions
{
    using System.Collections.Generic;

    public static class ListExtensions
    {
        public static List<T> Repeat<T>(this T value, int count)
        {
            var list = new List<T>(count);
            for (var i = 0; i < count; i++)
                list.Add(value);
            return list;
        }
    }
}