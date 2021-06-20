﻿namespace CleanArch.School.Application
{
    using System.Collections.Generic;

    public class EnrollStudentOutputData
    {
        public string Code { get; set; } = string.Empty;
        public ICollection<InvoiceOutputData> Invoices { get; set; } = new List<InvoiceOutputData>();
    }
}