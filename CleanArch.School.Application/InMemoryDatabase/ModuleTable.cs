namespace CleanArch.School.Application.InMemoryDatabase
{
    public class ModuleTable
    {
        public string Level { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MinimumAge { get; set; }
        public decimal Price { get; set; }
    }
}