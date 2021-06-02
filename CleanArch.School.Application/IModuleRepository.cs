namespace CleanArch.School.Application
{
    public interface IModuleRepository
    {
        ModuleTable FindByCode(string level, string module);
    }
}