namespace CleanArch.School.Application
{
    using InMemoryDatabase;

    public interface IClassroomRepository
    {
        ClassroomTable FindByCode(string level, string module, string classroom);
    }
}