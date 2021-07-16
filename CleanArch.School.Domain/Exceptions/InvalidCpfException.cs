namespace CleanArch.School.Domain.Exceptions
{
    public class InvalidCpfException : DomainException
    {
        internal InvalidCpfException(string message)
            : base(message) { }
    }
}