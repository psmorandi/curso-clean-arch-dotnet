namespace CleanArch.School.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Extensions;
    using Validators;

    public class EnrollStudent
    {
        private readonly ICpfValidator cpfValidator;
        private readonly Regex nameValidationRegex = new Regex("^([A-Za-z]+ )+([A-Za-z])+$", RegexOptions.Compiled);
        private readonly ICollection<Student> students;

        public EnrollStudent(ICpfValidator cpfValidator)
        {
            this.cpfValidator = cpfValidator;
            this.students = new List<Student>();
        }

        public void Execute(EnrollmentRequest enrollmentRequest)
        {
            var student = enrollmentRequest.Student;
            if (!this.nameValidationRegex.IsMatch(student.Name)) throw new Exception("Invalid student name.");
            if (!this.cpfValidator.IsValid(student.Cpf)) throw new Exception("Invalid student cpf.");
            if (this.IsAlreadyStored(student)) throw new Exception("Enrollment with duplicated student is not allowed.");
            this.students.Add(student);
        }

        private bool IsAlreadyStored(Student student)
            => this.students.Any(_ => _.Cpf.OnlyNumbers() == student.Cpf.OnlyNumbers());
    }
}