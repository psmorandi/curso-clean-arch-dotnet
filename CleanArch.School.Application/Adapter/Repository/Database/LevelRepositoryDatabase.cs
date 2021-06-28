namespace CleanArch.School.Application.Adapter.Repository.Database
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entity;
    using Domain.Repository;
    using Infra.Database;
    using Database = Data;

    public class LevelRepositoryDatabase : ILevelRepository
    {
        private readonly SchoolDbContext dbContext;
        private readonly IMapper mapper;

        public LevelRepositoryDatabase(SchoolDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Save(Level level)
        {
            this.dbContext.Levels.Add(this.mapper.Map<Database.Level>(level));
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Level> FindByCode(string code)
        {
            var level = await this.dbContext.Levels.FindAsync(code) ?? throw new Exception("Level not found.");
            return this.mapper.Map<Level>(level);
        }
    }
}