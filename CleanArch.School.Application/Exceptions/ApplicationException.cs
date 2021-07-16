namespace CleanArch.School.Application.Exceptions
{
    using System;

    public class ApplicationException : Exception
    {
        internal ApplicationException(string message)
            : base(message) { }
    }
}