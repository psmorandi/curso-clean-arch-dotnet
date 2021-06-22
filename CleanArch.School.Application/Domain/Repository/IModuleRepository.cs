namespace CleanArch.School.Application.Domain.Repository
{
    using Entity;

    public interface IModuleRepository
    {
        void Save(Module module);

        Module FindByCode(string level, string module);
    }
}