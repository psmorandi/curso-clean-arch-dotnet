namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class Enrollment
    {
        public Enrollment(Student student, Level level, Module module, Classroom classroom, int sequence, DateTime issueDate, int installments = 12)
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
        public DateTime IssueDate { get; }
        public EnrollmentCode Code { get; }
        public ICollection<Invoice> Invoices { get; }
        public EnrollStatus Status { get; private set; }

        private void GenerateInvoices(int installments)
        {
            var moduleValue = this.Module.Price;
            var installmentAmount = (moduleValue / installments).Truncate(2);
            for (var i = 1; i < installments; i++)
            {
                var invoice = new Invoice(this.Code.Value, i, this.IssueDate.Year, installmentAmount);
                this.Invoices.Add(invoice);
            }

            var lastInstallment = installmentAmount + (moduleValue - installmentAmount * installments);
            this.Invoices.Add(new Invoice(this.Code.Value, installments, this.IssueDate.Year, lastInstallment));
        }

        public decimal GetInvoiceBalance()
            => this.Invoices.Where(i => i.Status == InvoiceStatus.Pending).Sum(i => i.Amount);

        public Invoice GetInvoice(int month, int year)
            => this.Invoices.SingleOrDefault(i => i.Month == month && i.Year == year) ?? throw new Exception("Invoice not found.");

        public void SetEnrollmentStatus(EnrollStatus status) => this.Status = status;
    }
}