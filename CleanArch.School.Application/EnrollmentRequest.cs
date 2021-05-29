namespace CleanArch.School.Application
{
    using System;

    public class EnrollmentRequest
    {
        public string StudentName { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
    }
}