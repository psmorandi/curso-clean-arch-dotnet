namespace CleanArch.School.Application
{
    using System.Collections.Generic;
    using Domain.Entity;

    public class GetEnrollmentOutputData
    {
        public string StudentName { get; set; } = string.Empty;
        public string StudentCpf { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public EnrollStatus Status { get; set; } = EnrollStatus.Unknown;
        public ICollection<InvoiceOutputData> Invoices { get; set; } = new List<InvoiceOutputData>();
    }
}