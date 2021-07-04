using System.ComponentModel.DataAnnotations;

namespace CleanArch.School.API.Data
{
    public class PayInvoiceRequest
    {
        [Required]
        public decimal? Amount { get; set; }
    }
}