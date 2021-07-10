namespace CleanArch.School.Application.UseCase
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Extensions;
    using Factory;
    using Repository;

    public class GetAllEnrollments : IGetAllEnrollments
    {
        private readonly IMapper outputDataMapper;
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetAllEnrollments(IRepositoryAbstractFactory repositoryFactory, IMapper outputDataMapper)
        {
            this.outputDataMapper = outputDataMapper;
            this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();
        }

        public async Task<IEnumerable<GetEnrollmentOutputData>> Execute(DateOnly currentDate)
        {
            var enrollments = await this.enrollmentRepository.GetAll();
            return this.outputDataMapper.Map<IEnumerable<GetEnrollmentOutputData>>(enrollments, currentDate);
        }
    }
}