namespace CleanArch.School.Application
{
    public interface ILevelRepository
    {
        void Save(Level level);

        Level FindByCode(string code);
    }
}