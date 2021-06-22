namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entity;
    using Extensions;

    public class ClassroomRepositoryMemory : IClassroomRepository
    {
        private readonly ICollection<Classroom> classes;

        public ClassroomRepositoryMemory() => this.classes = new List<Classroom>();

        public void Save(Classroom classroom) => this.classes.Add(classroom);

        public Classroom FindByCode(string level, string module, string classroom) =>
            this.classes.SingleOrDefault(c => c.Level == level.ToUp() && c.Module == module.ToUp() && c.Code == classroom.ToUp())
            ?? throw new Exception("Invalid class.");
    }
}