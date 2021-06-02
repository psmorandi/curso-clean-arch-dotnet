namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class ModuleRepositoryMemory : IModuleRepository
    {
        private readonly ICollection<ModuleTable> modules;

        public ModuleRepositoryMemory()
        {
            var moduleAno1 = new ModuleTable
                             {
                                 Level = "EF1",
                                 Code = "1",
                                 Description = "1o Ano",
                                 MinimumAge = 6,
                                 Price = 15000
                             };
            var moduleAno2 = new ModuleTable
                             {
                                 Level = "EF1",
                                 Code = "2",
                                 Description = "2o Ano",
                                 MinimumAge = 7,
                                 Price = 15000
                             };
            var moduleAno3 = new ModuleTable
                             {
                                 Level = "EF1",
                                 Code = "3",
                                 Description = "3o Ano",
                                 MinimumAge = 8,
                                 Price = 15000
                             };
            var moduleAno4 = new ModuleTable
                             {
                                 Level = "EF1",
                                 Code = "4",
                                 Description = "4o Ano",
                                 MinimumAge = 9,
                                 Price = 15000
                             };
            var moduleAno5 = new ModuleTable
                             {
                                 Level = "EF1",
                                 Code = "5",
                                 Description = "5o Ano",
                                 MinimumAge = 10,
                                 Price = 15000
                             };
            var moduleAno6 = new ModuleTable
                             {
                                 Level = "EF2",
                                 Code = "6",
                                 Description = "6o Ano",
                                 MinimumAge = 11,
                                 Price = 14000
                             };
            var moduleAno7 = new ModuleTable
                             {
                                 Level = "EF2",
                                 Code = "7",
                                 Description = "7o Ano",
                                 MinimumAge = 12,
                                 Price = 14000
                             };
            var moduleAno8 = new ModuleTable
                             {
                                 Level = "EF2",
                                 Code = "8",
                                 Description = "8o Ano",
                                 MinimumAge = 13,
                                 Price = 14000
                             };
            var moduleAno9 = new ModuleTable
                             {
                                 Level = "EF2",
                                 Code = "9",
                                 Description = "9o Ano",
                                 MinimumAge = 14,
                                 Price = 14000
                             };
            var moduleEm1 = new ModuleTable
                            {
                                Level = "EM",
                                Code = "1",
                                Description = "1o Ano",
                                MinimumAge = 15,
                                Price = 17000
                            };
            var moduleEm2 = new ModuleTable
                            {
                                Level = "EM",
                                Code = "2",
                                Description = "2o Ano",
                                MinimumAge = 16,
                                Price = 17000
                            };
            var moduleEm3 = new ModuleTable
                            {
                                Level = "EM",
                                Code = "3",
                                Description = "3o Ano",
                                MinimumAge = 17,
                                Price = 17000
                            };

            this.modules = new List<ModuleTable>
                           {
                               moduleAno1, moduleAno2, moduleAno3, moduleAno4, moduleAno5, moduleAno6, moduleAno7, moduleAno8, moduleAno9, moduleEm1,
                               moduleEm2, moduleEm3
                           };
        }

        public ModuleTable FindByCode(string level, string module) =>
            this.modules.SingleOrDefault(m => m.Code == module.ToUp() && m.Level == level.ToUp()) ?? throw new Exception("Invalid Module.");
    }
}