namespace CleanArch.School.Application.Exceptions
{
    public class StudentAlreadyEnrolledException : ApplicationException
    {
        internal StudentAlreadyEnrolledException(string message)
            : base(message) { }
    }
}