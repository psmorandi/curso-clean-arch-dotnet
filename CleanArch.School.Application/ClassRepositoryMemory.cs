﻿namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class ClassRepositoryMemory : IClassRepository
    {
        private readonly ICollection<ClassTable> classes;

        public ClassRepositoryMemory() =>
            this.classes = new List<ClassTable>
                           {
                               new ClassTable
                               {
                                   Level = "EM",
                                   Module = "3",
                                   Code = "A",
                                   Capacity = 10
                               }
                           };

        public ClassTable FindByCode(string level, string module, string @class) =>
            this.classes.SingleOrDefault(c => c.Level == level.ToUp() && c.Module == module.ToUp() && c.Code == @class.ToUp())
            ?? throw new Exception("Invalid class.");
    }
}