﻿namespace CleanArch.School.Application
{
    using System.Collections.Generic;

    public class EnrollmentOutputData
    {
        public string StudentName { get; set; } = string.Empty;
        public string StudentCpf { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal InvoiceBalance { get; set; }        
        public EnrollStatus Status { get; set; } = EnrollStatus.Unknown;
        public ICollection<InvoiceOutputData> Invoices { get; set; } = new List<InvoiceOutputData>();
    }
}