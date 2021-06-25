﻿namespace CleanArch.School.Application.Adapter.Repository.Database
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entity;
    using Domain.Repository;
    using Infra.Database;
    using Database = Entities;

    public class ClassroomRepositoryDatabase : IClassroomRepository
    {
        private readonly SchoolDbContext dbContext;
        private readonly IMapper mapper;

        public ClassroomRepositoryDatabase(SchoolDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Save(Classroom classroom)
        {
            this.dbContext.Classrooms.Add(this.mapper.Map<Database.Classroom>(classroom));
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Classroom> FindByCode(string level, string module, string classroom)
        {
            var dbClassroom = await this.dbContext.Classrooms.FindAsync(classroom, level, module) ?? throw new Exception("Classroom not found");
            return this.mapper.Map<Classroom>(dbClassroom);
        }
    }
}