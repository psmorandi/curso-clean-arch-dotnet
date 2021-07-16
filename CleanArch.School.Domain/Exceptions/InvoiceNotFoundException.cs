namespace CleanArch.School.Domain.Exceptions
{
    public class InvoiceNotFoundException : DomainException
    {
        internal InvoiceNotFoundException(string message)
            : base(message) { }
    }
}