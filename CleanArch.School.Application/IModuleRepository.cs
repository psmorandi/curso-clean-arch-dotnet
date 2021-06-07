namespace CleanArch.School.Application
{
    public interface IModuleRepository
    {
        Module FindByCode(string level, string module);
    }
}