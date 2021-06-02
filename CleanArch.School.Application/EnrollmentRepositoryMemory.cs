namespace CleanArch.School.Application
{
    using System.Collections.Generic;
    using System.Linq;

    public class EnrollmentRepositoryMemory : IEnrollmentRepository
    {
        private readonly ICollection<Enrollment> enrollments;

        public EnrollmentRepositoryMemory() => this.enrollments = new List<Enrollment>();

        public void Save(Enrollment enrollment)
        {
            this.enrollments.Add(enrollment);
            enrollment.Id = this.enrollments.Count;
        }

        public ICollection<Enrollment> FindAllByClass(string level, string module, string @class) =>
            this.enrollments.Where(e => e.Level == level && e.Module == module && e.Class == @class).ToList();

        public Enrollment? FindByCpf(string cpf) => this.enrollments.SingleOrDefault(_ => _.Student.Cpf.Value == cpf);

        public int Count() => this.enrollments.Count;
    }
}