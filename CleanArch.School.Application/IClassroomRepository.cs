namespace CleanArch.School.Application
{
    public interface IClassroomRepository
    {
        void Save(Classroom classroom);

        Classroom FindByCode(string level, string module, string classroom);
    }
}