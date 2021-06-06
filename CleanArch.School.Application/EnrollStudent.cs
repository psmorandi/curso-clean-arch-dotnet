namespace CleanArch.School.Application
{
    using System;
    using Extensions;
    using InMemoryDatabase;

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
            if (this.IsAlreadyEnrolled(student)) throw new Exception("Enrollment with duplicated student is not allowed.");
            var level = this.levelRepository.FindByCode(enrollmentRequest.Level);
            var module = this.moduleRepository.FindByCode(level.Code, enrollmentRequest.Module);
            var classroom = this.classRepository.FindByCode(level.Code, module.Code, enrollmentRequest.Class);
            if (IsBellowMinimumAgeForModule(student, module)) throw new Exception("Student below minimum age.");
            if (IsClassFinished(classroom)) throw new Exception("Class is already finished.");
            if (IsClassAlreadyStarted(classroom)) throw new Exception("Class is already started.");
            if (!this.HasCapacityForStudent(enrollmentRequest, classroom)) throw new Exception("Class is over capacity.");
            return this.CreateEnrollment(student, level, module, classroom, enrollmentRequest.Installments);
        }

        private bool IsAlreadyEnrolled(Student student) => this.enrollmentRepository.FindByCpf(student.Cpf.Value) != null;

        private static bool IsBellowMinimumAgeForModule(Student student, ModuleTable module) => student.Age < module.MinimumAge;

        private static bool IsClassFinished(ClassroomTable classroom) => DateTime.Now.Date.After(classroom.EndDate);

        private static bool IsClassAlreadyStarted(ClassroomTable classroom)
        {
            var numberOfDaysOfClass = (classroom.EndDate - classroom.StartDate).Days;
            var daysForEnrollAllowance = numberOfDaysOfClass / 4;
            var limitDateToEnrollAfterClassStart = classroom.StartDate.AddDays(daysForEnrollAllowance);
            return DateTime.Now.Date.After(limitDateToEnrollAfterClassStart);
        }

        private bool HasCapacityForStudent(EnrollmentRequest request, ClassroomTable classroom) =>
            this.enrollmentRepository.FindAllByClass(request.Level, request.Module, request.Class).Count + 1 <= classroom.Capacity;

        private Enrollment CreateEnrollment(Student student, LevelTable level, ModuleTable module, ClassroomTable classroom, int installments)
        {
            var enrollment = new Enrollment(student, level.Code, module.Code, classroom.Code, installments, module.Price);
            this.enrollmentRepository.Save(enrollment);
            return enrollment;
        }
    }
}