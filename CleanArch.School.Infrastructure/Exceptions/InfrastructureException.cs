namespace CleanArch.School.Infrastructure.Exceptions
{
    using System;

    public class InfrastructureException : Exception
    {
        internal InfrastructureException(string message)
            : base(message) { }
    }
}