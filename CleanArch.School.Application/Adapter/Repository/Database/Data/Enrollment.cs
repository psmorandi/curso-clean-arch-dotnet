namespace CleanArch.School.Application.Adapter.Repository.Database.Data
{
    using System;

    public class Enrollment
    {
        public string Code { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Classroom { get; set; } = string.Empty;
        public string Student { get; set; } = string.Empty;
        public int Installments { get; set; }
        public DateTime IssueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Sequence { get; set; }
    }
}