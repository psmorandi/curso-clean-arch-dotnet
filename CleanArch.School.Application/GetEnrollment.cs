using AutoMapper;

namespace CleanArch.School.Application
{
    public class GetEnrollment
    {
        private readonly IMapper outputDataMapper;
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetEnrollment(IRepositoryAbstractFactory repositoryFactory, IMapper outputDataMapper)
        {
            this.outputDataMapper = outputDataMapper;
            this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();
        }

        public EnrollmentOutputData Execute(string code)
        {
            var enrollment = this.enrollmentRepository.FindByCode(code);
            return this.outputDataMapper.Map<EnrollmentOutputData>(enrollment);
        }
    }
}