namespace CleanArch.School.Application.Domain.UseCase
{
    using System;

    public class EnrollStudentInputData
    {
        public string StudentName { get; set; } = string.Empty;
        public string StudentCpf { get; set; } = string.Empty;
        public DateTime StudentBirthday { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public int Installments { get; set; } = 1;
    }
}