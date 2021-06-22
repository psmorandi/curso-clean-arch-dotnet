namespace CleanArch.School.Application.Domain.Repository
{
    using Entity;

    public interface ILevelRepository
    {
        void Save(Level level);

        Level FindByCode(string code);
    }
}