namespace CleanArch.School.Application.Adapter.Repository.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entity;
    using Domain.Repository;

    public class LevelRepositoryMemory : ILevelRepository
    {
        private readonly ICollection<Level> levels;

        public LevelRepositoryMemory() => this.levels = new List<Level>();

        public void Save(Level level) => this.levels.Add(level);

        public Level FindByCode(string code) =>
            this.levels.SingleOrDefault(l => l.Code == code) ?? throw new Exception("Invalid level");
    }
}