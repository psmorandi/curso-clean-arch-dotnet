namespace CleanArch.School.Application.Domain.UseCase
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Entity;
    using Extensions;
    using Factory;
    using Repository;

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

        public async Task<EnrollStudentOutputData> Execute(EnrollStudentInputData inputData, DateOnly currentDate)
        {
            var student = new Student(inputData.StudentName, inputData.StudentCpf, inputData.StudentBirthday);
            var level = await this.levelRepository.FindByCode(inputData.Level);
            var module = await this.moduleRepository.FindByCode(level.Code, inputData.Module);
            var classroom = await this.classRepository.FindByCode(level.Code, module.Code, inputData.Class);
            if (await this.IsAlreadyEnrolled(student)) throw new Exception("Enrollment with duplicated student is not allowed.");
            var numberOfStudentsInClass = (await this.enrollmentRepository.FindAllByClass(level.Code, module.Code, classroom.Code)).ToList().Count;
            if (numberOfStudentsInClass + 1 > classroom.Capacity) throw new Exception("Class is over capacity.");
            var sequence = 1 + await this.enrollmentRepository.Count();
            return this.CreateEnrollment(student, level, module, classroom, currentDate, sequence, inputData.Installments);
        }

        private async Task<bool> IsAlreadyEnrolled(Student student) => await this.enrollmentRepository.FindByCpf(student.Cpf.Value) != null;

        private EnrollStudentOutputData CreateEnrollment(
            Student student,
            Level level,
            Module module,
            Classroom classroom,
            DateOnly currentDate,
            int sequence,
            int installments)
        {
            var enrollment = new Enrollment(student, level, module, classroom, sequence, currentDate, installments);
            this.enrollmentRepository.Save(enrollment);
            return this.outputDataMapper.Map<EnrollStudentOutputData>(enrollment, currentDate);
        }
    }
}