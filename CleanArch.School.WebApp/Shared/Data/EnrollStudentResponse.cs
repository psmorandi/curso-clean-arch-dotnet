namespace CleanArch.School.WebApp.Shared.Data
{
    using System.Collections.Generic;

    public class EnrollStudentResponse
    {
        public string Code { get; set; } = string.Empty;
        public ICollection<InvoiceResponse> Invoices { get; set; } = new List<InvoiceResponse>();
    }
}