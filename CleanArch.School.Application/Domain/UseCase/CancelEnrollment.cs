namespace CleanArch.School.Application.Domain.UseCase
{
    using Factory;
    using Repository;
    using System.Threading.Tasks;

    public class CancelEnrollment
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public CancelEnrollment(IRepositoryAbstractFactory repositoryFactory)
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public async Task Execute(string code)
        {
            var enrollment = await this.enrollmentRepository.FindByCode(code);
            enrollment.Cancel();
        }
    }
}