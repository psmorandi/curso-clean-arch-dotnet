namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class ClassroomRepositoryMemory : IClassroomRepository
    {
        private readonly ICollection<Classroom> classes;

        public ClassroomRepositoryMemory() =>
            this.classes = new List<Classroom>
                           {
                               new Classroom("EM", "3", "A", 5, DateTime.Now.Date, DateTime.Now.Date.AddMonths(6)),
                               new Classroom("EM", "3", "B", 5, DateTime.Now.Date, DateTime.Now.Date.AddDays(30)),
                               new Classroom("EM", "3", "C", 5, DateTime.Now.Date, DateTime.Now.Date.AddMonths(1))
                           };

        public Classroom FindByCode(string level, string module, string @class) =>
            this.classes.SingleOrDefault(c => c.Level == level.ToUp() && c.Module == module.ToUp() && c.Code == @class.ToUp())
            ?? throw new Exception("Invalid class.");
    }
}