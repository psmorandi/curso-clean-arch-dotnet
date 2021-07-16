namespace CleanArch.School.Infrastructure.Exceptions
{
    public class LevelNotFoundException : InfrastructureException
    {
        internal LevelNotFoundException(string message)
            : base(message) { }
    }
}