namespace CleanArch.School.Application
{
    public interface ILevelRepository
    {
        Level FindByCode(string code);
    }
}