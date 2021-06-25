namespace CleanArch.School.Application.Adapter.Repository.Database.Entities
{
    public class Module
    {
        public string Code { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MinimumAge { get; set; }
        public int Price { get; set; }
    }
}