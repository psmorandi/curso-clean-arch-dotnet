namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using InMemoryDatabase;

    public class ClassroomRepositoryMemory : IClassRepository
    {
        private readonly ICollection<ClassroomTable> classes;

        public ClassroomRepositoryMemory() =>
            this.classes = new List<ClassroomTable>
                           {
                               new ClassroomTable
                               {
                                   Level = "EM",
                                   Module = "3",
                                   Code = "A",
                                   Capacity = 5,
                                   StartDate = DateTime.Now.Date,
                                   EndDate = DateTime.Now.Date.AddMonths(6)
                               },
                               new ClassroomTable
                               {
                                   Level = "EM",
                                   Module = "3",
                                   Code = "B",
                                   Capacity = 5,
                                   StartDate = DateTime.Now.Date,
                                   EndDate = DateTime.Now.Date.AddDays(30)
                               },
                               new ClassroomTable
                               {
                                   Level = "EM",
                                   Module = "3",
                                   Code = "C",
                                   Capacity = 5,
                                   StartDate = DateTime.Now.Date,
                                   EndDate = DateTime.Now.Date.AddMonths(1)
                               }
                           };

        public ClassroomTable FindByCode(string level, string module, string @class) =>
            this.classes.SingleOrDefault(c => c.Level == level.ToUp() && c.Module == module.ToUp() && c.Code == @class.ToUp())
            ?? throw new Exception("Invalid class.");
    }
}