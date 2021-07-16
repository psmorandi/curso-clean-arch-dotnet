namespace CleanArch.School.Infrastructure.Exceptions
{
    public class EnrollmentNotFoundException : InfrastructureException
    {
        internal EnrollmentNotFoundException(string message)
            : base(message) { }
    }
}