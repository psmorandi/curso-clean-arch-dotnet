namespace CleanArch.School.Application.Adapter.Repository.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Entity;
    using Domain.Repository;

    public class LevelRepositoryMemory : ILevelRepository
    {
        private readonly ICollection<Level> levels;

        public LevelRepositoryMemory() => this.levels = new List<Level>();

        public Task Save(Level level)
        {
            this.levels.Add(level);
            return Task.CompletedTask;
        }

        public Task<Level> FindByCode(string code) =>
            Task.FromResult(this.levels.SingleOrDefault(l => l.Code == code) ?? throw new Exception("Invalid level"));
    }
}