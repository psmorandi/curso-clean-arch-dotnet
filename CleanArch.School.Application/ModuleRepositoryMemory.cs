namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entity;
    using Extensions;

    public class ModuleRepositoryMemory : IModuleRepository
    {
        private readonly ICollection<Module> modules;

        public ModuleRepositoryMemory() => this.modules = new List<Module>();

        public void Save(Module module) => this.modules.Add(module);

        public Module FindByCode(string level, string module) =>
            this.modules.SingleOrDefault(m => m.Code == module.ToUp() && m.Level == level.ToUp()) ?? throw new Exception("Invalid Module.");
    }
}