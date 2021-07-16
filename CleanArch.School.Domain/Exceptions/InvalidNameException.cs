namespace CleanArch.School.Domain.Exceptions
{
    public class InvalidNameException : DomainException
    {
        internal InvalidNameException(string message)
            : base(message) { }
    }
}