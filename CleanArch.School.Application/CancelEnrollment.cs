namespace CleanArch.School.Application
{
    public class CancelEnrollment
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public CancelEnrollment(IRepositoryAbstractFactory repositoryFactory)
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public void Execute(CancelEnrollmentRequest request)
        {
            var enrollment = this.enrollmentRepository.FindByCode(request.EnrollmentCode);
            enrollment.SetEnrollmentStatus(EnrollStatus.Cancelled);
        }
    }
}