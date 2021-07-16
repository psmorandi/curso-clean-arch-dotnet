namespace CleanArch.School.Infrastructure.Exceptions
{
    public class ModuleNotFoundException : InfrastructureException
    {
        internal ModuleNotFoundException(string message)
            : base(message) { }
    }
}