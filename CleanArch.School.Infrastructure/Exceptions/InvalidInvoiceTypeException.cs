namespace CleanArch.School.Infrastructure.Exceptions
{
    public class InvalidInvoiceTypeException : InfrastructureException
    {
        internal InvalidInvoiceTypeException(string message)
            : base(message) { }
    }
}