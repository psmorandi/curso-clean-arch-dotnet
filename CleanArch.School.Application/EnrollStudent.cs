namespace CleanArch.School.Application
{
    using System;
    using System.Linq;

    public class EnrollStudent
    {
        private readonly Storage storage;

        public EnrollStudent(Storage storage) => this.storage = storage;

        public Enrollment Execute(EnrollmentRequest enrollmentRequest)
        {
            var student = new Student(enrollmentRequest.StudentName, enrollmentRequest.Cpf, enrollmentRequest.Birthday);
            if (this.IsAlreadyEnrolled(student)) throw new Exception("Enrollment with duplicated student is not allowed.");
            var @class = this.FindClass(
                enrollmentRequest.Level.ToUpperInvariant(),
                enrollmentRequest.Module.ToUpperInvariant(),
                enrollmentRequest.Class);
            if (IsBellowMinimumAgeForClass(student, @class)) throw new Exception("Student below minimum age.");
            if (!HasCapacityForStudent(@class)) throw new Exception("Class is over capacity.");
            return this.CreateEnrollment(student, @class);
        }

        private bool IsAlreadyEnrolled(Student student) => this.storage.Data.Enrollments.Any(_ => _.Student.Cpf.Value == student.Cpf.Value);

        private ClassTable FindClass(string level, string module, string @class) =>
            this.storage.Data.Classes.SingleOrDefault(c => c.Code == @class && c.Level.Code == level && c.Module.Code == module)
            ?? throw new Exception("Invalid class.");

        private static bool IsBellowMinimumAgeForClass(Student student, ClassTable @class) => student.Age < @class.Module.MinimumAge;

        private static bool HasCapacityForStudent(ClassTable @class) => @class.Capacity - 1 >= 0;

        private Enrollment CreateEnrollment(Student student, ClassTable @class)
        {
            @class.Capacity--;
            var enrollment = new Enrollment(student, @class.Level.Code, @class.Module.Code, @class.Code);
            this.storage.Data.Enrollments.Add(enrollment);
            enrollment.Sequence = this.storage.Data.Enrollments.Count;
            return enrollment;
        }
    }
}