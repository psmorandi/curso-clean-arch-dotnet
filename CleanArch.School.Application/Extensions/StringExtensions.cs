namespace CleanArch.School.Application.Extensions
{
    using System;
    using System.Linq;

    public static class StringExtensions
    {
        public static string OnlyNumbers(this string str) => new(str.Where(char.IsDigit).ToArray());

        public static string ToUp(this string str) => str.ToUpperInvariant();

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[length];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++) stringChars[i] = chars[random.Next(chars.Length)];

            return new string(stringChars);
        }
    }
}