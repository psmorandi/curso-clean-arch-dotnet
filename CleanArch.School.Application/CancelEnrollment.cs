namespace CleanArch.School.Application
{
    public class CancelEnrollment
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public CancelEnrollment(IEnrollmentRepository enrollmentRepository) => this.enrollmentRepository = enrollmentRepository;

        public void Execute(CancelEnrollmentRequest request)
        {
            var enrollment = this.enrollmentRepository.FindByCode(request.EnrollmentCode);
            enrollment.SetEnrollmentStatus(EnrollStatus.Cancelled);
        }
    }
}