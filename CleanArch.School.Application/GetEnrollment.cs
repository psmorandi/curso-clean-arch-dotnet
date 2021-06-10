namespace CleanArch.School.Application
{
    using System;

    public class GetEnrollment
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetEnrollment(IEnrollmentRepository enrollmentRepository) => this.enrollmentRepository = enrollmentRepository;

        public Enrollment Execute(GetEnrollmentRequest request) =>
            this.enrollmentRepository.FindByCode(request.EnrollmentCode);
    }
}