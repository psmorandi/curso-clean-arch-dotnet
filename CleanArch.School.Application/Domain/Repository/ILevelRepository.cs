namespace CleanArch.School.Application.Domain.Repository
{
    using System.Threading.Tasks;
    using Entity;

    public interface ILevelRepository
    {
        Task Save(Level level);

        Task<Level> FindByCode(string code);
    }
}