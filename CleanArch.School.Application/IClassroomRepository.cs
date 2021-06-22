namespace CleanArch.School.Application
{
    using Domain.Entity;

    public interface IClassroomRepository
    {
        void Save(Classroom classroom);

        Classroom FindByCode(string level, string module, string classroom);
    }
}