namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LevelRepositoryMemory : ILevelRepository
    {
        private readonly ICollection<Level> levels;

        public LevelRepositoryMemory()
        {   
            this.levels = new List<Level>();
        }

        public void Save(Level level) => this.levels.Add(level);

        public Level FindByCode(string code) =>
            this.levels.SingleOrDefault(l => l.Code == code) ?? throw new Exception("Invalid level");
    }
}