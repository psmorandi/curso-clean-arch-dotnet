namespace CleanArch.School.Infrastructure.Database.Data
{
    using System;

    public class Invoice
    {
        public string Enrollment { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
    }
}