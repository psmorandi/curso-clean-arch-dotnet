namespace CleanArch.School.WebApp.Client.Extensions
{
    using Domain.Entity;

    public static class EnrollStatusExtensions
    {
        public static string AsString(this EnrollStatus enrollStatus) => enrollStatus switch
        {
            EnrollStatus.Active => "Enrolled",
            EnrollStatus.Cancelled => "Cancelled",
            _ => "Unknown"
        };
    }
}