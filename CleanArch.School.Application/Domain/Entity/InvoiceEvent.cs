namespace CleanArch.School.Application.Domain.Entity
{
    public abstract class InvoiceEvent
    {
        protected InvoiceEvent(decimal amount) => this.Amount = amount;

        public decimal Amount { get; }
        public abstract InvoiceEventType Type { get; }
    }
}