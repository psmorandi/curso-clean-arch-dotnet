﻿namespace CleanArch.School.Application.Adapter.Repository.Database
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entity;
    using Domain.Repository;
    using Infra.Database;
    using Database = Data;

    public class ModuleRepositoryDatabase : IModuleRepository
    {
        private readonly SchoolDbContext dbContext;
        private readonly IMapper mapper;

        public ModuleRepositoryDatabase(SchoolDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Save(Module module)
        {
            this.dbContext.Modules.Add(this.mapper.Map<Database.Module>(module));
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Module> FindByCode(string level, string module)
        {
            var storedModule = await this.dbContext.Modules.FindAsync(module, level) ?? throw new Exception("Module not found.");
            return this.mapper.Map<Module>(storedModule);
        }
    }
}