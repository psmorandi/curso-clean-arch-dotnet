namespace CleanArch.School.Application
{
    using InMemoryDatabase;

    public interface IClassRepository
    {
        ClassroomTable FindByCode(string level, string module, string @class);
    }
}