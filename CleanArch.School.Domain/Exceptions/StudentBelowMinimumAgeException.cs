namespace CleanArch.School.Domain.Exceptions
{
    public class StudentBelowMinimumAgeException : DomainException
    {
        internal StudentBelowMinimumAgeException(string message)
            : base(message) { }
    }
}