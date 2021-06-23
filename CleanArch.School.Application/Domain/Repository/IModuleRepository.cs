namespace CleanArch.School.Application.Domain.Repository
{
    using Entity;
    using System.Threading.Tasks;

    public interface IModuleRepository
    {
        Task Save(Module module);

        Task<Module> FindByCode(string level, string module);
    }
}