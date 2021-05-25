namespace CleanArch.School.Application
{
    public class Enrollment
    {
        public Enrollment(Student student)
        {
            this.Student = student;
        }

        public Student Student { get; }
    }
}