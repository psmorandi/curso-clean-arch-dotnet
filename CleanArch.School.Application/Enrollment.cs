namespace CleanArch.School.Application
{
    using System;

    public class Enrollment
    {
        public Enrollment(Student student, string level, string module, string classroom, int installments, decimal moduleValue)
        {
            this.Student = student;
            this.Level = level;
            this.Module = module;
            this.Class = classroom;
            this.EnrollDate = DateTime.UtcNow;
            this.Invoice = new Invoice(installments, moduleValue);
        }

        public int Id { get; set; }
        public Student Student { get; }
        public string Class { get; }
        public string Module { get; }
        public string Level { get; }
        public DateTime EnrollDate { get; }
        public string EnrollmentCode => $"{this.EnrollDate.Year:0000}{this.Level}{this.Module}{this.Class}{this.Id:0000}";
        public Invoice Invoice { get; }
    }
}