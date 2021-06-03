using System;

namespace CleanArch.School.Application
{
    public class LevelTable
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class ModuleTable
    {
        public string Level { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MinimumAge { get; set; }
        public decimal Price { get; set; }
    }

    public class ClassTable
    {
        public string Level { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}