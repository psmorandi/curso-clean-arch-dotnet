﻿namespace CleanArch.School.Application.Domain.UseCase
{
    using System;
    using Extensions;
    using Factory;
    using global::AutoMapper;
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

        public GetEnrollmentOutputData Execute(string code, DateOnly currentDate)
        {
            var enrollment = this.enrollmentRepository.FindByCode(code);
            return this.outputDataMapper.Map<GetEnrollmentOutputData>(enrollment, currentDate);
        }
    }
}