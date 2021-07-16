namespace CleanArch.School.Domain
{
    using System;

    public class DomainException : Exception
    {
        internal DomainException(string message)
            : base(message) { }
    }
}