namespace CleanArch.School.Application
{
    using System.Collections.Generic;

    public class Storage
    {
        public Data Data { get; } = new Data();
    }

    public class Data
    {
        public Data()
        {
            var levelEF1 = new Level
                           {
                               Code = "EF1",
                               Description = "Ensino Fundamental I"
                           };

            var levelEF2 = new Level
                           {
                               Code = "EF2",
                               Description = "Ensino Fundamental II"
                           };
            var levelEM = new Level
                          {
                              Code = "EM",
                              Description = "Ensino Médio"
                          };
            this.Levels = new List<Level> { levelEF1, levelEF2, levelEM };

            var moduleAno1 = new Module
                             {
                                 Level = levelEF1,
                                 Code = "1",
                                 Description = "1o Ano",
                                 MinimumAge = 6,
                                 Price = 15000
                             };
            var moduleAno2 = new Module
                             {
                                 Level = levelEF1,
                                 Code = "2",
                                 Description = "2o Ano",
                                 MinimumAge = 7,
                                 Price = 15000
                             };
            var moduleAno3 = new Module
                             {
                                 Level = levelEF1,
                                 Code = "3",
                                 Description = "3o Ano",
                                 MinimumAge = 8,
                                 Price = 15000
                             };
            var moduleAno4 = new Module
                             {
                                 Level = levelEF1,
                                 Code = "4",
                                 Description = "4o Ano",
                                 MinimumAge = 9,
                                 Price = 15000
                             };
            var moduleAno5 = new Module
                             {
                                 Level = levelEF1,
                                 Code = "5",
                                 Description = "5o Ano",
                                 MinimumAge = 10,
                                 Price = 15000
                             };
            var moduleAno6 = new Module
                             {
                                 Level = levelEF2,
                                 Code = "6",
                                 Description = "6o Ano",
                                 MinimumAge = 11,
                                 Price = 14000
                             };
            var moduleAno7 = new Module
                             {
                                 Level = levelEF2,
                                 Code = "7",
                                 Description = "7o Ano",
                                 MinimumAge = 12,
                                 Price = 14000
                             };
            var moduleAno8 = new Module
                             {
                                 Level = levelEF2,
                                 Code = "8",
                                 Description = "8o Ano",
                                 MinimumAge = 13,
                                 Price = 14000
                             };
            var moduleAno9 = new Module
                             {
                                 Level = levelEF2,
                                 Code = "9",
                                 Description = "9o Ano",
                                 MinimumAge = 14,
                                 Price = 14000
                             };
            var moduleEM1 = new Module
                            {
                                Level = levelEM,
                                Code = "1",
                                Description = "1o Ano",
                                MinimumAge = 15,
                                Price = 17000
                            };
            var moduleEM2 = new Module
                            {
                                Level = levelEM,
                                Code = "2",
                                Description = "2o Ano",
                                MinimumAge = 16,
                                Price = 17000
                            };
            var moduleEM3 = new Module
                            {
                                Level = levelEM,
                                Code = "3",
                                Description = "3o Ano",
                                MinimumAge = 17,
                                Price = 17000
                            };
            this.Modules = new List<Module>
                           {
                               moduleAno1, moduleAno2, moduleAno3, moduleAno4, moduleAno5, moduleAno6, moduleAno7, moduleAno8, moduleAno9, moduleEM1,
                               moduleEM2, moduleEM3
                           };
            this.Classes = new List<Class>
                           {
                               new Class
                               {
                                   Level = levelEM,
                                   Module = moduleEM3,
                                   Code = "A",
                                   Capacity = 10
                               }
                           };
            this.Enrollments = new List<Enrollment>();
        }

        public List<Enrollment> Enrollments { get; }
        public ICollection<Level> Levels { get; }
        public ICollection<Module> Modules { get; }
        public ICollection<Class> Classes { get; }
    }

    public class Level
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class Module
    {
        public Level Level { get; set; } = new Level();
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MinimumAge { get; set; }
        public decimal Price { get; set; }
    }

    public class Class
    {
        public Level Level { get; set; } = new Level();
        public Module Module { get; set; } = new Module();
        public string Code { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}