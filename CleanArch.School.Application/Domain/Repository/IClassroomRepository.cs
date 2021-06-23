namespace CleanArch.School.Application.Domain.Repository
{
    using System.Threading.Tasks;
    using Entity;

    public interface IClassroomRepository
    {
        Task Save(Classroom classroom);

        Task<Classroom> FindByCode(string level, string module, string classroom);
    }
}