namespace CleanArch.School.Application.Domain.UseCase
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Extensions;
    using Factory;
    using Repository;

    public class GetEnrollment
    {
        private readonly IMapper outputDataMapper;
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetEnrollment(IRepositoryAbstractFactory repositoryFactory, IMapper outputDataMapper)
        {
            this.outputDataMapper = outputDataMapper;
            this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();
        }

        public async Task<GetEnrollmentOutputData> Execute(string code, DateOnly currentDate)
        {
            var enrollment = await this.enrollmentRepository.FindByCode(code);
            return this.outputDataMapper.Map<GetEnrollmentOutputData>(enrollment, currentDate);
        }
    }
}