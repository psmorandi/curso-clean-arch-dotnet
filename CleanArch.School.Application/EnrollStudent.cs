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
            var @class = this.FindClass(enrollmentRequest.Level.ToUp(), enrollmentRequest.Module.ToUp(), enrollmentRequest.Class.ToUp());
            if (IsBellowMinimumAgeForClass(student, @class)) throw new Exception("Student below minimum age.");
            if (!this.HasCapacityForStudent(enrollmentRequest, @class)) throw new Exception("Class is over capacity.");
            return this.CreateEnrollment(student, @class);
        }

        private bool IsAlreadyEnrolled(Student student) => this.storage.Data.Enrollments.Any(_ => _.Student.Cpf.Value == student.Cpf.Value);

        private ClassTable FindClass(string level, string module, string @class) =>
            this.storage.Data.Classes.SingleOrDefault(c => c.Code == @class && c.Level.Code == level && c.Module.Code == module)
            ?? throw new Exception("Invalid class.");

        private static bool IsBellowMinimumAgeForClass(Student student, ClassTable @class) => student.Age < @class.Module.MinimumAge;

        private bool HasCapacityForStudent(EnrollmentRequest request, ClassTable @class) =>
            @class.Capacity >= this.storage.Data.Enrollments.Count(
                e => e.Class == request.Class &&
                     e.Level == request.Level &&
                     e.Module == request.Module) + 1;

        private Enrollment CreateEnrollment(Student student, ClassTable @class)
        {
            var enrollment = new Enrollment(student, @class.Level.Code, @class.Module.Code, @class.Code);
            this.storage.Data.Enrollments.Add(enrollment);
            enrollment.Id = this.storage.Data.Enrollments.Count;
            return enrollment;
        }
    }
}