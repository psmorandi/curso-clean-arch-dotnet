namespace CleanArch.School.Application
{
    using System;

    public class Enrollment
    {
        public Enrollment(Student student, Level level, Module module, Classroom classroom, int sequence, DateTime issueDate, int installments, decimal moduleValue)
        {
            if (IsBellowMinimumAgeForModule(student, module)) throw new Exception("Student below minimum age.");
            this.Student = student;
            this.Level = level;
            this.Module = module;
            this.Class = classroom;
            this.IssueDate = issueDate;
            this.Invoice = new Invoice(installments, moduleValue);
            this.Sequence = sequence;
            this.Code = new EnrollmentCode(level, module, classroom, sequence, issueDate);
        }

        public int Sequence { get; }
        public Student Student { get; }
        public Classroom Class { get; }
        public Module Module { get; }
        public Level Level { get; }
        public DateTime IssueDate { get; }
        public EnrollmentCode Code { get; }
        public Invoice Invoice { get; }

        private static bool IsBellowMinimumAgeForModule(Student student, Module module) => student.Age < module.MinimumAge;
    }
}