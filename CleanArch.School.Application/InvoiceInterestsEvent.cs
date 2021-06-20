namespace CleanArch.School.Application
{
    public class InvoiceInterestsEvent : InvoiceEvent
    {
        public InvoiceInterestsEvent(decimal amount) : base(amount)
        {
        }

        public override InvoiceEventType Type => InvoiceEventType.Interests;
    }
}
