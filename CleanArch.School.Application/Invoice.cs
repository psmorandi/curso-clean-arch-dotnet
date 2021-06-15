namespace CleanArch.School.Application
{
    using System.Collections.Generic;
    using System.Linq;

    public class Invoice
    {
        public Invoice(string code, int month, int year, decimal amount)
        {
            this.Code = code;
            this.Month = month;
            this.Year = year;
            this.Amount = amount;
            this.InvoiceEvents = new List<InvoiceEvent>();
        }

        public string Code { get; }
        public int Month { get; }
        public int Year { get; }
        public decimal Amount { get; }
        public ICollection<InvoiceEvent> InvoiceEvents { get; }

        public void AddEvent(InvoiceEvent invoiceEvent)
        {
            this.InvoiceEvents.Add(invoiceEvent);
        }

        public decimal GetBalance()
        {
            var totalPaid = this.InvoiceEvents
                .Where(e => e.Type == InvoiceEventType.Payment)
                .Sum(e => e.Amount);
            return this.Amount - totalPaid;
        }
    }
}