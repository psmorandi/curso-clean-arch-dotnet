namespace CleanArch.School.Application.UseCase
{
    using System.Threading.Tasks;

    public interface ICancelEnrollment
    {
        Task Execute(string code);
    }
}