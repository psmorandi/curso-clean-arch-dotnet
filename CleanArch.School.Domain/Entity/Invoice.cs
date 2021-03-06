namespace CleanArch.School.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TypeExtensions;

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
        public DateOnly DueDate { get; }
        public decimal Amount { get; }
        public ICollection<InvoiceEvent> InvoiceEvents { get; private set; }

        public void AddEvent(InvoiceEvent invoiceEvent)
        {
            this.InvoiceEvents.Add(invoiceEvent);
        }

        public decimal GetBalance()
        {
            var totalPaid = this.InvoiceEvents
                .Sum(e => e.Type == InvoiceEventType.Payment ? e.Amount : -e.Amount);
            return this.Amount - totalPaid;
        }

        public InvoiceStatus GetStatus(DateOnly currentDate)
        {
            if (this.GetBalance() == 0) return InvoiceStatus.Paid;
            return currentDate > this.DueDate ? InvoiceStatus.Overdue : InvoiceStatus.Open;
        }

        public decimal GetPenalty(DateOnly currentDate)
        {
            if (this.GetStatus(currentDate) != InvoiceStatus.Overdue) return 0;
            var balance = this.GetBalance();
            return Math.Round(balance * INVOICE_PENALTY.ToPercentage(), 2).Truncate(2);
        }

        public decimal GetInterests(DateOnly currentDate)
        {
            if (this.GetStatus(currentDate) != InvoiceStatus.Overdue) return 0;
            var balance = this.GetBalance();
            var numberOfOverdueDays = (currentDate.ToDateTime() - this.DueDate.ToDateTime()).Days;
            return Math.Round(balance * numberOfOverdueDays * DAILY_INTERESTS.ToPercentage(), 2).Truncate(2);
        }

        public static Invoice Load(string code, int day, int month, int year, decimal amount, ICollection<InvoiceEvent> invoiceEvents)
        {
            var invoice = new Invoice(code, day, month, year, amount);
            invoice.InvoiceEvents = invoiceEvents;
            return invoice;
        }
    }
}