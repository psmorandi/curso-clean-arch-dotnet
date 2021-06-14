namespace CleanArch.School.Application
{
    public class GetEnrollment
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetEnrollment(IRepositoryAbstractFactory repositoryFactory)
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public Enrollment Execute(GetEnrollmentRequest request) =>
            this.enrollmentRepository.FindByCode(request.EnrollmentCode);
    }
}