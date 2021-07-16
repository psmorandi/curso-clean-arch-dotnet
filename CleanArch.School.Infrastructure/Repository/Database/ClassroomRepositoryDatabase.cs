namespace CleanArch.School.Infrastructure.Repository.Database
{
    using System.Threading.Tasks;
    using Application.Repository;
    using AutoMapper;
    using Domain.Entity;
    using Exceptions;
    using Infrastructure.Database;
    using Database = Infrastructure.Database.Data;

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
            var dbClassroom = await this.dbContext.Classrooms.FindAsync(classroom, level, module) ?? throw new ClassroomNotFoundException("Classroom not found");
            return this.mapper.Map<Classroom>(dbClassroom);
        }
    }
}