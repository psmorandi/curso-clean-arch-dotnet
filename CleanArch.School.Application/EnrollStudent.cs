namespace CleanArch.School.Application
{
    using System;
    using global::AutoMapper;
    using CleanArch.School.Application.Extensions;
    using Domain.Entity;

    public class EnrollStudent
    {
        private readonly IMapper outputDataMapper;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly ILevelRepository levelRepository;
        private readonly IModuleRepository moduleRepository;
        private readonly IClassroomRepository classRepository;

        public EnrollStudent(IRepositoryAbstractFactory repositoryFactory, IMapper outputDataMapper)
        {
            this.outputDataMapper = outputDataMapper;
            this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();
            this.levelRepository = repositoryFactory.CreateLevelRepository();
            this.moduleRepository = repositoryFactory.CreateModuleRepository();
            this.classRepository = repositoryFactory.CreateClassroomRepository();
        }

        public EnrollStudentOutputData Execute(EnrollStudentInputData inputData, DateOnly currentDate)
        {
            var student = new Student(inputData.StudentName, inputData.StudentCpf, inputData.StudentBirthday);
            var level = this.levelRepository.FindByCode(inputData.Level);
            var module = this.moduleRepository.FindByCode(level.Code, inputData.Module);
            var classroom = this.classRepository.FindByCode(level.Code, module.Code, inputData.Class);
            if (this.IsAlreadyEnrolled(student)) throw new Exception("Enrollment with duplicated student is not allowed.");
            var numberOfStudentsInClass = this.enrollmentRepository.FindAllByClass(level.Code, module.Code, classroom.Code).Count;
            if (numberOfStudentsInClass + 1 > classroom.Capacity) throw new Exception("Class is over capacity.");
            return this.CreateEnrollment(student, level, module, classroom, currentDate, this.enrollmentRepository.Count() + 1, inputData.Installments);
        }

        private bool IsAlreadyEnrolled(Student student) => this.enrollmentRepository.FindByCpf(student.Cpf.Value) != null;

        private EnrollStudentOutputData CreateEnrollment(Student student, Level level, Module module, Classroom classroom, DateOnly currentDate, int sequence, int installments)
        {
            var enrollment = new Enrollment(student, level, module, classroom, sequence, currentDate, installments);
            this.enrollmentRepository.Save(enrollment);
            return this.outputDataMapper.Map<EnrollStudentOutputData>(enrollment, currentDate);
        }
    }
}