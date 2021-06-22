namespace CleanArch.School.Application.Domain.Entity
{
    public class InvoicePaidEvent : InvoiceEvent
    {
        public InvoicePaidEvent(decimal amount)
            : base(amount) { }

        public override InvoiceEventType Type => InvoiceEventType.Payment;
    }
}