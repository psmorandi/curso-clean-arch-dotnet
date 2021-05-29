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
            var sequence = $"{(this.storage.Data.Enrollments.Count + 1):0000}";
            var fullYear = DateTime.Now.Year;
            var level = enrollmentRequest.Level.ToUpperInvariant();
            var module = enrollmentRequest.Module.ToUpperInvariant();
            var @class = enrollmentRequest.Class;
            var enrollCode = $"{fullYear:0000}{level}{module}{@class}{sequence}";
            var requestedClass = this.storage.Data.Classes.SingleOrDefault(c => c.Code == @class && c.Level.Code == level && c.Module.Code == module)??throw new Exception("Invalid class.");
            if (student.Age < requestedClass.Module.MinimumAge) throw new Exception("Student below minimum age.");
            if(requestedClass.Capacity - 1 < 0) throw new Exception("Class is over capacity.");
            requestedClass.Capacity--;
            var enrollment = new Enrollment(student, enrollCode);
            this.storage.Data.Enrollments.Add(enrollment);
            return enrollment;
        }

        private bool IsAlreadyEnrolled(Student student) => this.storage.Data.Enrollments.Any(_ => _.Student.Cpf.Value == student.Cpf.Value);
    }
}