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
        private readonly IClassRepository classRepository;

        public EnrollStudent(
            IEnrollmentRepository enrollmentRepository,
            ILevelRepository levelRepository,
            IModuleRepository moduleRepository,
            IClassRepository classRepository)
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
            var @class = this.classRepository.FindByCode(level.Code, module.Code, enrollmentRequest.Class);
            if (IsBellowMinimumAgeForModule(student, module)) throw new Exception("Student below minimum age.");
            if (IsClassFinished(@class)) throw new Exception("Class is already finished.");
            if (IsClassAlreadyStarted(@class)) throw new Exception("Class is already started.");
            if (!this.HasCapacityForStudent(enrollmentRequest, @class)) throw new Exception("Class is over capacity.");
            return this.CreateEnrollment(student, level, module, @class);
        }

        private bool IsAlreadyEnrolled(Student student) => this.enrollmentRepository.FindByCpf(student.Cpf.Value) != null;

        private static bool IsBellowMinimumAgeForModule(Student student, ModuleTable module) => student.Age < module.MinimumAge;

        private static bool IsClassFinished(ClassroomTable @class) => DateTime.Now.Date.After(@class.EndDate);

        private static bool IsClassAlreadyStarted(ClassroomTable @class)
        {
            var numberOfDaysOfClass = (@class.EndDate - @class.StartDate).Days;
            var daysForEnrollAllowance = numberOfDaysOfClass / 4;
            var limitDateToEnrollAfterClassStart = @class.StartDate.AddDays(daysForEnrollAllowance);
            return DateTime.Now.Date.After(limitDateToEnrollAfterClassStart);
        }

        private bool HasCapacityForStudent(EnrollmentRequest request, ClassroomTable @class) =>
            this.enrollmentRepository.FindAllByClass(request.Level, request.Module, request.Class).Count + 1 <= @class.Capacity;

        private Enrollment CreateEnrollment(Student student, LevelTable level, ModuleTable module, ClassroomTable @class)
        {
            var enrollment = new Enrollment(student, level.Code, module.Code, @class.Code);
            this.enrollmentRepository.Save(enrollment);
            return enrollment;
        }
    }
}