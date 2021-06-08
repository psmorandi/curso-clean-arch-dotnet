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
        }

        public string Code { get; }
        public int Month { get; }
        public int Year { get; }
        public decimal Amount { get; }
    }
}