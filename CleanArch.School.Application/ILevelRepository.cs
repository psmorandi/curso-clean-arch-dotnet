namespace CleanArch.School.Application
{
    using Domain.Entity;

    public interface ILevelRepository
    {
        void Save(Level level);

        Level FindByCode(string code);
    }
}