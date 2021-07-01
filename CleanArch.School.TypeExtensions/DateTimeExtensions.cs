namespace CleanArch.School.TypeExtensions
{
    using System;

    public static class DateTimeExtensions
    {
        public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);

        public static DateTime ToDateTime(this DateOnly date) => date.ToDateTime(new TimeOnly(0));

        public static DateOnly UtcNow(this DateOnly date) => DateTime.UtcNow.ToDateOnly();
    }
}