namespace CleanArch.School.Application.UseCase
{
    using System.Threading.Tasks;
    using Factory;
    using Repository;

    public class CancelEnrollment : ICancelEnrollment
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public CancelEnrollment(IRepositoryAbstractFactory repositoryFactory)
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public async Task Execute(string code)
        {
            var enrollment = await this.enrollmentRepository.FindByCode(code);
            enrollment.Cancel();
            await this.enrollmentRepository.Update(enrollment);
        }
    }
}