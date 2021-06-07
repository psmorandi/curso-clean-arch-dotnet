namespace CleanArch.School.Application
{
    public interface IClassroomRepository
    {
        Classroom FindByCode(string level, string module, string classroom);
    }
}