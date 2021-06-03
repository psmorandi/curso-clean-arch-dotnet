namespace CleanArch.School.Application
{
    using InMemoryDatabase;

    public interface IModuleRepository
    {
        ModuleTable FindByCode(string level, string module);
    }
}