namespace CleanArch.School.Application.Extensions
{
    using System.Linq;

    public static class CpfExtensions
    {
        public static string OnlyNumbers(this string str)
        {
            return new string(str.Where(char.IsDigit).ToArray());
        }
    }
}