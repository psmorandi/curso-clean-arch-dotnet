namespace CleanArch.School.Application.Domain.Entity
{
    public class InvoiceInterestsEvent : InvoiceEvent
    {
        public InvoiceInterestsEvent(decimal amount) : base(amount)
        {
        }

        public override InvoiceEventType Type => InvoiceEventType.Interests;
    }
}
