namespace CleanArch.School.Application.Adapter.Repository.Database.Data
{
    public class InvoiceEvent
    {
        public long Id { get; set; }
        public string Enrollment { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}