namespace CleanArch.School.Application
{
    using System;

    public class Enrollment
    {
        public Enrollment(Student student, string level, string module, string @class)
        {
            this.Student = student;
            this.Level = level;
            this.Module = module;
            this.Class = @class;
            this.EnrollDate = DateTime.UtcNow;
        }

        public int Sequence { get; set; }
        public Student Student { get; }
        public string Class { get; }
        public string Module { get; }
        public string Level { get; }
        public DateTime EnrollDate { get; }
        public string EnrollmentCode => $"{this.EnrollDate.Year:0000}{this.Level}{this.Module}{this.Class}{this.Sequence:0000}";
    }
}