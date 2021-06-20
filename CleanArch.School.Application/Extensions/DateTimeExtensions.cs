using System;

namespace CleanArch.School.Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool Before(this DateTime date, DateTime otherDate) => date.CompareTo(otherDate) < 0;
        public static bool After(this DateTime date, DateTime otherDate) => date.CompareTo(otherDate) > 0;

        public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);
    }
}