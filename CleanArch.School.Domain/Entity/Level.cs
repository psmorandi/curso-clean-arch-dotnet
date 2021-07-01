namespace CleanArch.School.Domain.Entity
{
    public class Level
    {
        public Level(string code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        public string Code { get; }
        public string Description { get; }
    }
}