namespace CleanArch.School.WebApp.Shared.Data
{
    using System;
    using System.Text.Json.Serialization;
    using Domain.Entity;

    public class InvoiceResponse
    {
        [JsonConverter(typeof(JsonDateOnlyConverter))]
        public DateOnly DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal Interests { get; set; }
        public decimal Penalty { get; set; }
    }
}