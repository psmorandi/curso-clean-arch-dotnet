namespace CleanArch.School.Application
{
    using Domain.Entity;

    public interface IModuleRepository
    {
        void Save(Module module);

        Module FindByCode(string level, string module);
    }
}