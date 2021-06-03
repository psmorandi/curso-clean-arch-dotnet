namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using InMemoryDatabase;

    public class LevelRepositoryMemory : ILevelRepository
    {
        private readonly ICollection<LevelTable> levels;

        public LevelRepositoryMemory()
        {
            var levelEf1 = new LevelTable
                           {
                               Code = "EF1",
                               Description = "Ensino Fundamental I"
                           };

            var levelEf2 = new LevelTable
                           {
                               Code = "EF2",
                               Description = "Ensino Fundamental II"
                           };
            var levelEm = new LevelTable
                          {
                              Code = "EM",
                              Description = "Ensino Médio"
                          };
            this.levels = new List<LevelTable> { levelEf1, levelEf2, levelEm };
        }

        public LevelTable FindByCode(string code) => 
            this.levels.SingleOrDefault(l => l.Code == code) ?? throw new Exception("Invalid level");
    }
}