namespace CleanArch.School.Infrastructure.Repository.Memory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Repository;
    using Domain.Entity;
    using Exceptions;

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
            Task.FromResult(this.levels.SingleOrDefault(l => l.Code == code) ?? throw new LevelNotFoundException("Invalid level"));
    }
}