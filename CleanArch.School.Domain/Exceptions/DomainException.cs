namespace CleanArch.School.Domain.Exceptions
{
    using System;

    public class DomainException : Exception
    {
        internal DomainException(string message)
            : base(message) { }
    }
}