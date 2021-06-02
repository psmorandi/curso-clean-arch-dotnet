namespace CleanArch.School.Application
{
    public interface IClassRepository
    {
        ClassTable FindByCode(string level, string module, string @class);
    }
}