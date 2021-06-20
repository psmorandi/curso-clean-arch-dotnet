namespace CleanArch.School.Application
{
    using CleanArch.School.Application.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Invoice
    {
        private static readonly int INVOICE_PENALTY = 10;
        private static readonly int DAILY_INTERESTS = 1;

        public Invoice(string code, int day, int month, int year, decimal amount)
        {
            this.Code = code;
            this.DueDate = new DateOnly(year, month, day);
            this.Amount = amount;
            this.InvoiceEvents = new List<InvoiceEvent>();
        }

        public string Code { get; }
        public DateOnly DueDate { get; set; }
        public decimal Amount { get; }
        public ICollection<InvoiceEvent> InvoiceEvents { get; }

        public void AddEvent(InvoiceEvent invoiceEvent)
        {
            this.InvoiceEvents.Add(invoiceEvent);
        }

        public decimal GetBalance()
        {
            var totalPaid = this.InvoiceEvents
                .Sum(e => e.Type == InvoiceEventType.Payment? e.Amount : -e.Amount);
            return this.Amount - totalPaid;
        }

        public InvoiceStatus GetStatus()
        {
            if(this.GetBalance() == 0) return InvoiceStatus.Paid;
            if(DateTime.UtcNow.ToDateOnly() <= this.DueDate) return InvoiceStatus.Open;
            return InvoiceStatus.Overdue;
        }

        public decimal GetPenalty() => this.GetStatus() == InvoiceStatus.Overdue? 
            (this.Amount * INVOICE_PENALTY.ToPercentage()).Truncate(2) : 0;

        public decimal GetInterests() 
        {
            if(this.GetStatus() != InvoiceStatus.Overdue) return 0;
            var numberOfOverdueDays = (DateTime.UtcNow.Date - this.DueDate.ToDateTime()).Days;
            return (this.Amount * numberOfOverdueDays * DAILY_INTERESTS.ToPercentage()).Truncate(2);
        }
    }
}