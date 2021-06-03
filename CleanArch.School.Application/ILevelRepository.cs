namespace CleanArch.School.Application
{
    using InMemoryDatabase;

    public interface ILevelRepository
    {
        LevelTable FindByCode(string code);
    }
}