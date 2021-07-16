namespace CleanArch.School.Infrastructure.Exceptions
{
    public class ClassroomNotFoundException : InfrastructureException
    {
        internal ClassroomNotFoundException(string message)
            : base(message) { }
    }
}