namespace CleanArch.School.WebApp.Client.Extensions
{
    using System.Globalization;

    public static class DecimalExtensions
    {
        public static string FormatAsBrlCurrency(this decimal value) => value.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
    }
}