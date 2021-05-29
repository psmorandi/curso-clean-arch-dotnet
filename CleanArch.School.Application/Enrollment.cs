namespace CleanArch.School.Application
{
    public class Enrollment
    {
        public Enrollment(Student student, string enrollmentCode)
        {
            this.Student = student;
            this.EnrollmentCode = enrollmentCode;
        }

        public Student Student { get; }
        public string EnrollmentCode { get; }
    }
}