namespace CleanArch.School.Infrastructure.Database.Data.Extensions
{
    using Domain.Entity;

    public static class InvoiceTypeExtensions
    {
        public static string GetValue(this InvoiceEventType type)
            => type switch
            {
                InvoiceEventType.Payment => "payment",
                InvoiceEventType.Penalty => "penalty",
                InvoiceEventType.Interests => "interests",
                _ => "unknown"
            };

        public static InvoiceEventType ToInvoiceEventType(this string status)
            => status switch
            {
                "payment" => InvoiceEventType.Payment,
                "penalty" => InvoiceEventType.Penalty,
                "interests" => InvoiceEventType.Interests,
                _ => InvoiceEventType.Unknown
            };
    }
}