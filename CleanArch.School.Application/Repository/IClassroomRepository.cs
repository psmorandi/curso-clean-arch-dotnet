namespace CleanArch.School.Application.Repository
{
    using System.Threading.Tasks;
    using Domain.Entity;

    public interface IClassroomRepository
    {
        Task Save(Classroom classroom);

        Task<Classroom> FindByCode(string level, string module, string classroom);
    }
}