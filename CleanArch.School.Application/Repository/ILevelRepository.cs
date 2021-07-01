namespace CleanArch.School.Application.Repository
{
    using System.Threading.Tasks;
    using Domain.Entity;

    public interface ILevelRepository
    {
        Task Save(Level level);

        Task<Level> FindByCode(string code);
    }
}