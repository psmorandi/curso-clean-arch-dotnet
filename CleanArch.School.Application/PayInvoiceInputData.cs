﻿using System;

namespace CleanArch.School.Application
{
    public class PayInvoiceInputData
    {
        public string Code { get; set; } = string.Empty;
        public int Month { get; set; } = 1;
        public int Year { get; set; } = DateTime.Now.Year;
        public decimal Amount { get; set; }
        public DateOnly PaymentDate { get; set; }
    }
}