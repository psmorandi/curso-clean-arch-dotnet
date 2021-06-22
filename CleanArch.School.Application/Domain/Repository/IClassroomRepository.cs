namespace CleanArch.School.Application.Domain.Repository
{
    using Entity;

    public interface IClassroomRepository
    {
        void Save(Classroom classroom);

        Classroom FindByCode(string level, string module, string classroom);
    }
}