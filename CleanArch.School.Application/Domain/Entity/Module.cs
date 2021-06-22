namespace CleanArch.School.Application.Domain.Entity
{
    public class Module
    {
        public Module(string level, string code, string description, int minimumAge, decimal price)
        {
            this.Level = level;
            this.Code = code;
            this.Description = description;
            this.MinimumAge = minimumAge;
            this.Price = price;
        }

        public string Level { get; }
        public string Code { get; }
        public string Description { get; }
        public int MinimumAge { get; }
        public decimal Price { get; }
    }
}