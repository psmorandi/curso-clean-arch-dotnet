namespace CleanArch.School.Domain.Exceptions
{
    public class ClassroomAlreadyFinishedException : DomainException
    {
        internal ClassroomAlreadyFinishedException(string message)
            : base(message) { }
    }
}