namespace CleanArch.School.Application.Adapter.Repository.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Entity;
    using Domain.Repository;
    using Extensions;

    public class ModuleRepositoryMemory : IModuleRepository
    {
        private readonly ICollection<Module> modules;

        public ModuleRepositoryMemory() => this.modules = new List<Module>();

        public Task Save(Module module)
        {
            this.modules.Add(module);
            return Task.CompletedTask;
        }

        public Task<Module> FindByCode(string level, string module) =>
            Task.FromResult(this.modules.SingleOrDefault(m => m.Code == module.ToUp() && m.Level == level.ToUp()) ?? throw new Exception("Invalid Module."));
    }
}