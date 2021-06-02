namespace CleanArch.School.Application
{
    public interface ILevelRepository
    {
        LevelTable FindByCode(string code);
    }
}