namespace CleanArch.School.Infrastructure.Repository.Memory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Repository;
    using Domain.Entity;
    using Exceptions;
    using TypeExtensions;

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
            Task.FromResult(
                this.modules.SingleOrDefault(m => m.Code == module.ToUp() && m.Level == level.ToUp()) ?? throw new ModuleNotFoundException("Invalid Module."));
    }
}