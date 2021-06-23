﻿namespace CleanArch.School.Application.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class Enrollment
    {
        public Enrollment(Student student, Level level, Module module, Classroom classroom, int sequence, DateOnly issueDate, int installments = 12)
        {
            if (student.Age < module.MinimumAge) throw new Exception("Student below minimum age.");
            if (classroom.IsFinished(issueDate)) throw new Exception("Class is already finished.");
            if (classroom.GetProgress(issueDate) > 25) throw new Exception("Class is already started.");
            this.Student = student;
            this.Level = level;
            this.Module = module;
            this.Class = classroom;
            this.IssueDate = issueDate;
            this.Invoices = new List<Invoice>(installments);
            this.Sequence = sequence;
            this.Code = new EnrollmentCode(level, module, classroom, sequence, issueDate);
            this.GenerateInvoices(installments);
            this.Status = EnrollStatus.Active;
        }

        public int Sequence { get; }
        public Student Student { get; }
        public Classroom Class { get; }
        public Module Module { get; }
        public Level Level { get; }
        public DateOnly IssueDate { get; }
        public EnrollmentCode Code { get; }
        public ICollection<Invoice> Invoices { get; }
        public EnrollStatus Status { get; private set; }

        private void GenerateInvoices(int installments)
        {
            var moduleValue = this.Module.Price;
            var installmentAmount = (moduleValue / installments).Truncate(2);
            var lastInstallment = installmentAmount + (moduleValue - installmentAmount * installments);
            for (var i = 0; i < installments; i++)
            {
                var dueDate = this.IssueDate.AddMonths(i);
                var amount = i != installments - 1 ? installmentAmount : lastInstallment;
                var invoice = new Invoice(this.Code.Value, dueDate.Day, dueDate.Month, dueDate.Year, amount);
                this.Invoices.Add(invoice);
            }
        }

        public decimal GetInvoiceBalance()
            => this.Invoices.Sum(i => i.GetBalance());

        private Invoice GetInvoice(int month, int year)
            => this.Invoices.SingleOrDefault(i => i.DueDate.Month == month && i.DueDate.Year == year) ?? throw new Exception("Invoice not found.");

        public void PayInvoice(int month, int year, decimal amount, DateOnly paymentDate)
        {
            var invoice = this.GetInvoice(month, year);
            if (invoice.GetStatus(paymentDate) == InvoiceStatus.Overdue)
            {
                var penalty = invoice.GetPenalty(paymentDate);
                var interests = invoice.GetInterests(paymentDate);
                invoice.AddEvent(new InvoicePenaltyEvent(penalty));
                invoice.AddEvent(new InvoiceInterestsEvent(interests));
            }

            invoice.AddEvent(new InvoicePaidEvent(amount));
        }

        public void Cancel() => this.Status = EnrollStatus.Cancelled;
    }
}