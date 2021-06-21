﻿namespace CleanArch.School.UnitTests
{
    using System;
    using Application;
    using Application.Extensions;
    using AutoMapper;

    // ReSharper disable InconsistentNaming
    public abstract class BaseEnrollmentTests : BaseTests
    {
        protected readonly EnrollStudent enrollStudent;
        protected readonly GetEnrollment getEnrollment;
        protected readonly IRepositoryAbstractFactory repositoryFactory;
        protected readonly ILevelRepository levelRepository;
        protected readonly IModuleRepository moduleRepository;
        protected readonly IClassroomRepository classroomRepository;
        protected readonly IEnrollmentRepository enrollmentRepository;

        protected BaseEnrollmentTests()
        {
            var configuration =
                new MapperConfiguration(cfg => cfg.AddMaps(typeof(Enrollment).Assembly));
            var outputDataMapper = configuration.CreateMapper();
            this.repositoryFactory = new RepositoryMemoryAbstractFactory();
            this.levelRepository = this.repositoryFactory.CreateLevelRepository();
            this.moduleRepository = this.repositoryFactory.CreateModuleRepository();
            this.classroomRepository = this.repositoryFactory.CreateClassroomRepository();
            this.enrollmentRepository = this.repositoryFactory.CreateEnrollmentRepository();
            this.enrollStudent = new EnrollStudent(this.repositoryFactory, outputDataMapper);
            this.getEnrollment = new GetEnrollment(this.repositoryFactory, outputDataMapper);
        }

        protected EnrollStudentInputData CreateEnrollmentRequest(string cpf, string level, string module, string classroom, int installments = 1)
        {
            var minimumAge = this.moduleRepository.FindByCode(level, module).MinimumAge;
            var classCode = this.classroomRepository.FindByCode(level, module, classroom).Code;
            return new EnrollStudentInputData
                   {
                       StudentName = $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(7)}",
                       StudentCpf = cpf,
                       StudentBirthday = DateTime.Now.Date.AddYears(-minimumAge),
                       Level = level,
                       Module = module,
                       Class = classCode,
                       Installments = installments
                   };
        }

        protected EnrollStudentOutputData CreateRandomEnrollment(DateOnly issueDate)
        {
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "1", "1o Ano", 15, 17000));
            this.classroomRepository.Save(new Classroom("EM", "1", "A", 10, issueDate, issueDate.AddMonths(12)));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "1", "A", 12);
            return this.enrollStudent.Execute(enrollmentRequest, issueDate);
        }

        protected string CreateEnrollmentWith(DateOnly issueDate)
        {
            var level = new Level("EM", "Ensino Médio");
            this.levelRepository.Save(level);
            var module = new Module("EM", "1", "1o Ano", 15, 17000);
            this.moduleRepository.Save(module);
            var classroom = new Classroom("EM", "1", "A", 10, issueDate, issueDate.AddMonths(12));
            this.classroomRepository.Save(classroom);
            var student = new Student(
                $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(7)}",
                "755.525.774-26",
                DateTime.Now.Date.AddYears(-15));
            var enrollment = new Enrollment(student,level, module,classroom,1,issueDate,12);
            this.enrollmentRepository.Save(enrollment);
            return enrollment.Code.Value;
        }

        protected GetEnrollmentOutputData GetEnrollment(string code, DateOnly refDate) => this.getEnrollment.Execute(code, refDate);
    }
}