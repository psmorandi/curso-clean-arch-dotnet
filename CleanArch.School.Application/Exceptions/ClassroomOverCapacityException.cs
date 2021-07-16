namespace CleanArch.School.Application.Exceptions
{
    public class ClassroomOverCapacityException : ApplicationException
    {
        internal ClassroomOverCapacityException(string message)
            : base(message) { }
    }
}