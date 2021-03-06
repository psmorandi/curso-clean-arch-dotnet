namespace CleanArch.School.Infrastructure.Repository.Memory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Repository;
    using Domain.Entity;
    using Exceptions;
    using TypeExtensions;

    public class ClassroomRepositoryMemory : IClassroomRepository
    {
        private readonly ICollection<Classroom> classes;

        public ClassroomRepositoryMemory() => this.classes = new List<Classroom>();

        public Task Save(Classroom classroom)
        {
            this.classes.Add(classroom);
            return Task.CompletedTask;
        }

        public Task<Classroom> FindByCode(string level, string module, string classroom) =>
            Task.FromResult(
                this.classes.SingleOrDefault(c => c.Level == level.ToUp() && c.Module == module.ToUp() && c.Code == classroom.ToUp())
                ?? throw new ClassroomNotFoundException("Invalid class."));
    }
}