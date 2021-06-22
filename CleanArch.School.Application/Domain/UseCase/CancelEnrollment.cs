namespace CleanArch.School.Application.Domain.UseCase
{
    using Factory;
    using Repository;

    public class CancelEnrollment
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public CancelEnrollment(IRepositoryAbstractFactory repositoryFactory)
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public void Execute(string code)
        {
            var enrollment = this.enrollmentRepository.FindByCode(code);
            enrollment.Cancel();
        }
    }
}