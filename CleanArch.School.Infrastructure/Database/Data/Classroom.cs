namespace CleanArch.School.Infrastructure.Database.Data
{
    using System;

    public class Classroom
    {
        public string Code { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}