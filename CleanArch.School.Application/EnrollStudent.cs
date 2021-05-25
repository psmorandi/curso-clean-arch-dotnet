namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EnrollStudent
    {
        private readonly ICollection<Enrollment> enrollments;

        public EnrollStudent()
        {
            this.enrollments = new List<Enrollment>();
        }

        public Enrollment Execute(EnrollmentRequest enrollmentRequest)
        {
            var student = new Student(enrollmentRequest.StudentName, enrollmentRequest.Cpf);
            if (this.IsAlreadyEnrolled(student)) throw new Exception("Enrollment with duplicated student is not allowed.");
            var enrollment = new Enrollment(student);
            this.enrollments.Add(enrollment);
            return enrollment;
        }

        private bool IsAlreadyEnrolled(Student student) => this.enrollments.Any(_ => _.Student.Cpf.Value == student.Cpf.Value);
    }
}