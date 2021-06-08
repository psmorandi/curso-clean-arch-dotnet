namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class ModuleRepositoryMemory : IModuleRepository
    {
        private readonly ICollection<Module> modules;

        public ModuleRepositoryMemory()
        {
            var moduleAno1 = new Module("EF1","1","1o Ano",6,5000);
            var moduleAno2 = new Module("EF1", "2", "2o Ano", 7, 15000);
            var moduleAno3 = new Module("EF1", "3", "3o Ano", 8, 15000);
            var moduleAno4 = new Module("EF1", "4", "4o Ano", 9, 15000);
            var moduleAno5 = new Module("EF1", "5", "5o Ano", 10, 15000);
            var moduleAno6 = new Module("EF2", "6", "6o Ano", 11, 14000);
            var moduleAno7 = new Module("EF2", "7", "7o Ano", 12, 14000);
            var moduleAno8 = new Module("EF2", "8", "8o Ano", 13, 14000);
            var moduleAno9 = new Module("EF2", "9", "9o Ano", 14, 14000);
            var moduleEm1 = new Module("EM", "1", "1o Ano", 15, 17000);
            var moduleEm2 = new Module("EM", "2", "2o Ano", 16, 17000);
            var moduleEm3 = new Module("EM", "3", "3o Ano", 17, 17000);
            this.modules = new List<Module>
                           {
                               moduleAno1, moduleAno2, moduleAno3, moduleAno4, moduleAno5, moduleAno6, moduleAno7, moduleAno8, moduleAno9, moduleEm1,
                               moduleEm2, moduleEm3
                           };
        }

        public Module FindByCode(string level, string module) =>
            this.modules.SingleOrDefault(m => m.Code == module.ToUp() && m.Level == level.ToUp()) ?? throw new Exception("Invalid Module.");
    }
}