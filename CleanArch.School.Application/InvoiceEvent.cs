namespace CleanArch.School.Application
{
    public abstract class InvoiceEvent
    {
        protected InvoiceEvent(decimal amount) => this.Amount = amount;

        public decimal Amount { get; }
        public abstract InvoiceEventType Type { get; }
    }
}