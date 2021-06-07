namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LevelRepositoryMemory : ILevelRepository
    {
        private readonly ICollection<Level> levels;

        public LevelRepositoryMemory()
        {
            var levelEf1 = new Level("EF1", "Ensino Fundamental I");
            var levelEf2 = new Level("EF2", "Ensino Fundamental II");
            var levelEm = new Level("EM", "Ensino Médio");
            this.levels = new List<Level> { levelEf1, levelEf2, levelEm };
        }

        public Level FindByCode(string code) =>
            this.levels.SingleOrDefault(l => l.Code == code) ?? throw new Exception("Invalid level");
    }
}