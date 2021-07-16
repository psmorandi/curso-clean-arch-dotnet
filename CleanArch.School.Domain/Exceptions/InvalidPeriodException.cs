namespace CleanArch.School.Domain.Exceptions
{
    public class InvalidPeriodException : DomainException
    {
        internal InvalidPeriodException(string message)
            : base(message) { }
    }
}