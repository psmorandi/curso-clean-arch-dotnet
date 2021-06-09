namespace CleanArch.School.Application
{
    public interface IModuleRepository
    {
        void Save(Module module);

        Module FindByCode(string level, string module);
    }
}