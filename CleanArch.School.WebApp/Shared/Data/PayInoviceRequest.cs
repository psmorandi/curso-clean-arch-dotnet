namespace CleanArch.School.WebApp.Shared.Data
{
    using System.ComponentModel.DataAnnotations;

    public class PayInvoiceRequest
    {
        [Required]
        public decimal? Amount { get; set; }
    }
}