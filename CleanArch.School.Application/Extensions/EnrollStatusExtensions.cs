namespace CleanArch.School.Application.Extensions
{
    using Domain.Entity;

    public static class EnrollStatusExtensions
    {
        public static string GetValue(this EnrollStatus status)
            => status switch
            {
                EnrollStatus.Active => "active",
                EnrollStatus.Cancelled => "cancelled",
                _ => "unknown"
            };

        public static EnrollStatus ToEnrollStatus(this string status)
            => status switch
            {
                "active" => EnrollStatus.Active,
                "cancelled" => EnrollStatus.Cancelled,
                _ => EnrollStatus.Unknown
            };
    }
}