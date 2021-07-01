namespace CleanArch.School.Application.Repository
{
    using System.Threading.Tasks;
    using Domain.Entity;

    public interface IModuleRepository
    {
        Task Save(Module module);

        Task<Module> FindByCode(string level, string module);
    }
}