namespace CleanArch.School.Application
{
    using System;
    using System.Linq;
    using Extensions;

    public class EnrollStudent
    {
        private readonly Storage storage;

        public EnrollStudent(Storage storage) => this.storage = storage;

        public Enrollment Execute(EnrollmentRequest enrollmentRequest)
        {
            var student = new Student(enrollmentRequest.StudentName, enrollmentRequest.Cpf, enrollmentRequest.Birthday);
            if (this.IsAlreadyEnrolled(student)) throw new Exception("Enrollment with duplicated student is not allowed.");
            var module = this.FindModule(enrollmentRequest);
            var @class = this.FindClass(enrollmentRequest);
            if (IsBellowMinimumAgeForModule(student, module)) throw new Exception("Student below minimum age.");
            if (!this.HasCapacityForStudent(enrollmentRequest, @class)) throw new Exception("Class is over capacity.");
            return this.CreateEnrollment(student, @class);
        }

        private bool IsAlreadyEnrolled(Student student) => this.storage.Data.Enrollments.Any(_ => _.Student.Cpf.Value == student.Cpf.Value);

        private ModuleTable FindModule(EnrollmentRequest request) =>
            this.storage.Data.Modules.SingleOrDefault(m => m.Code == request.Module.ToUp() && m.Level.Code == request.Level.ToUp())
            ?? throw new Exception("Invalid Module.");

        private ClassTable FindClass(EnrollmentRequest request) =>
            this.storage.Data.Classes.SingleOrDefault(
                c => c.Level.Code == request.Level.ToUp() &&
                     c.Module.Code == request.Module.ToUp() &&
                     c.Code == request.Class.ToUp())
            ?? throw new Exception("Invalid class.");

        private static bool IsBellowMinimumAgeForModule(Student student, ModuleTable module) => student.Age < module.MinimumAge;

        private bool HasCapacityForStudent(EnrollmentRequest request, ClassTable @class) =>
            this.storage.Data.Enrollments.Count(
                e => e.Class == request.Class &&
                     e.Level == request.Level &&
                     e.Module == request.Module) + 1 <= @class.Capacity;

        private Enrollment CreateEnrollment(Student student, ClassTable @class)
        {
            var enrollment = new Enrollment(student, @class.Level.Code, @class.Module.Code, @class.Code);
            this.storage.Data.Enrollments.Add(enrollment);
            enrollment.Id = this.storage.Data.Enrollments.Count;
            return enrollment;
        }
    }
}