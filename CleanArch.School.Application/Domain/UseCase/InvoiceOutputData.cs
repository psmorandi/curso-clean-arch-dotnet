namespace CleanArch.School.Application.Domain.UseCase
{
    using System;
    using Entity;

    public class InvoiceOutputData
    {
        public DateOnly DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal Interests { get; set; }
        public decimal Penalty { get; set; }
    }
}