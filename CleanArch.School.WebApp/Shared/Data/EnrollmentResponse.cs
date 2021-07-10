namespace CleanArch.School.WebApp.Shared.Data
{
    using System.Collections.Generic;
    using Domain.Entity;

    public class EnrollmentResponse
    {
        public string StudentName { get; set; } = string.Empty;
        public string StudentCpf { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public EnrollStatus Status { get; set; } = EnrollStatus.Unknown;
        public ICollection<InvoiceResponse> Invoices { get; set; } = new List<InvoiceResponse>();
    }
}