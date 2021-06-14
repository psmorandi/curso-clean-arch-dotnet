namespace CleanArch.School.Application
{
    using System;

    public class EnrollStudent
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly ILevelRepository levelRepository;
        private readonly IModuleRepository moduleRepository;
        private readonly IClassroomRepository classRepository;

        public EnrollStudent(IRepositoryAbstractFactory repositoryFactory)
        {
            this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();
            this.levelRepository = repositoryFactory.CreateLevelRepository();
            this.moduleRepository = repositoryFactory.CreateModuleRepository();
            this.classRepository = repositoryFactory.CreateClassroomRepository();
        }

        public Enrollment Execute(EnrollmentRequest enrollmentRequest)
        {
            var student = new Student(enrollmentRequest.StudentName, enrollmentRequest.Cpf, enrollmentRequest.Birthday);
            var level = this.levelRepository.FindByCode(enrollmentRequest.Level);
            var module = this.moduleRepository.FindByCode(level.Code, enrollmentRequest.Module);
            var classroom = this.classRepository.FindByCode(level.Code, module.Code, enrollmentRequest.Class);
            if (this.IsAlreadyEnrolled(student)) throw new Exception("Enrollment with duplicated student is not allowed.");
            var numberOfStudentsInClass = this.enrollmentRepository.FindAllByClass(level.Code, module.Code, classroom.Code).Count;
            if (numberOfStudentsInClass + 1 > classroom.Capacity) throw new Exception("Class is over capacity.");
            return this.CreateEnrollment(student, level, module, classroom, this.enrollmentRepository.Count() + 1, enrollmentRequest.Installments);
        }

        private bool IsAlreadyEnrolled(Student student) => this.enrollmentRepository.FindByCpf(student.Cpf.Value) != null;

        private Enrollment CreateEnrollment(Student student, Level level, Module module, Classroom classroom, int sequence, int installments)
        {
            var enrollment = new Enrollment(student, level, module, classroom, sequence, DateTime.UtcNow.Date, installments);
            this.enrollmentRepository.Save(enrollment);
            return enrollment;
        }
    }
}