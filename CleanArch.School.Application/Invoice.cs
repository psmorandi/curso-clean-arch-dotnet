namespace CleanArch.School.Application
{
    public class Invoice
    {
        public Invoice(string code, int month, int year, decimal amount)
        {
            this.Code = code;
            this.Month = month;
            this.Year = year;
            this.Amount = amount;
            this.Status = InvoiceStatus.Pending;
        }

        public string Code { get; }
        public int Month { get; }
        public int Year { get; }
        public decimal Amount { get; }
        public decimal AmountPaid { get; private set; }
        public InvoiceStatus Status { get; private set; }

        public void Pay(decimal amount)
        {
            this.AmountPaid = amount;
            this.Status = InvoiceStatus.Paid;
        }
    }
}