namespace CleanArch.School.Application.Domain.Entity
{
    public class InvoicePenaltyEvent : InvoiceEvent
    {
        public InvoicePenaltyEvent(decimal amount)
            : base(amount) { }

        public override InvoiceEventType Type => InvoiceEventType.Penalty;
    }
}