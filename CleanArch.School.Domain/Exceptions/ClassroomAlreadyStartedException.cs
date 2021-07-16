namespace CleanArch.School.Domain.Exceptions
{
    public class ClassroomAlreadyStartedException : DomainException
    {
        internal ClassroomAlreadyStartedException(string message)
            : base(message) { }
    }
}