namespace CleanArch.School.Application
{
    using System;

    public class EnrollStudent
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly ILevelRepository levelRepository;
        private readonly IModuleRepository moduleRepository;
        private readonly IClassroomRepository classRepository;

        public EnrollStudent(
            IEnrollmentRepository enrollmentRepository,
            ILevelRepository levelRepository,
            IModuleRepository moduleRepository,
            IClassroomRepository classRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
            this.levelRepository = levelRepository;
            this.moduleRepository = moduleRepository;
            this.classRepository = classRepository;
        }

        public Enrollment Execute(EnrollmentRequest enrollmentRequest)
        {
            var student = new Student(enrollmentRequest.StudentName, enrollmentRequest.Cpf, enrollmentRequest.Birthday);
            var level = this.levelRepository.FindByCode(enrollmentRequest.Level);
            var module = this.moduleRepository.FindByCode(level.Code, enrollmentRequest.Module);
            var classroom = this.classRepository.FindByCode(level.Code, module.Code, enrollmentRequest.Class);
            if (this.IsAlreadyEnrolled(student)) throw new Exception("Enrollment with duplicated student is not allowed.");
            var numberOfEnrollments = this.enrollmentRepository.FindAllByClass(level.Code, module.Code, classroom.Code).Count;
            if (!HasCapacityForStudent(numberOfEnrollments, classroom)) throw new Exception("Class is over capacity.");
            return this.CreateEnrollment(student, level, module, classroom, numberOfEnrollments + 1, enrollmentRequest.Installments);
        }

        private bool IsAlreadyEnrolled(Student student) => this.enrollmentRepository.FindByCpf(student.Cpf.Value) != null;

        private static bool HasCapacityForStudent(int numberOfEnrollments, Classroom classroom) => numberOfEnrollments + 1 <= classroom.Capacity;

        private Enrollment CreateEnrollment(Student student, Level level, Module module, Classroom classroom, int sequence, int installments)
        {
            var enrollment = new Enrollment(student, level, module, classroom, sequence, DateTime.UtcNow.Date, installments);
            this.enrollmentRepository.Save(enrollment);
            return enrollment;
        }
    }
}