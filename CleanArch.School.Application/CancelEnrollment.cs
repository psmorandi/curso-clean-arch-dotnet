namespace CleanArch.School.Application
{
    public class CancelEnrollment
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public CancelEnrollment(IRepositoryAbstractFactory repositoryFactory)
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public void Execute(string code)
        {
            var enrollment = this.enrollmentRepository.FindByCode(code);
            enrollment.SetEnrollmentStatus(EnrollStatus.Cancelled);
        }
    }
}