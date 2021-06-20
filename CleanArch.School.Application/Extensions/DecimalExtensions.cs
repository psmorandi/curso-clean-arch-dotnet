namespace CleanArch.School.Application.Extensions
{
    using System;

    public static class DecimalExtensions
    {
        public static decimal Truncate(this decimal value, int precision)
        {
            var step = new decimal(Math.Pow(10, precision));
            return Math.Truncate(step * value) / step;
        }

        public static decimal ToPercentage(this int value) => value / new decimal(100);
    }
}