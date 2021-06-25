namespace CleanArch.School.Application.Adapter.Repository.Database.Entities
{
    public class InvoiceEvent
    {
        public string Enrollment { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}