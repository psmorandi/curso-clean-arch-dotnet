namespace CleanArch.School.Infrastructure.Repository.Memory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Repository;
    using Domain.Entity;
    using Exceptions;

    public class EnrollmentRepositoryMemory : IEnrollmentRepository
    {
        private readonly ICollection<Enrollment> enrollments;

        public EnrollmentRepositoryMemory() => this.enrollments = new List<Enrollment>();

        public Task Save(Enrollment enrollment)
        {
            this.enrollments.Add(enrollment);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Enrollment>> GetAll() => Task.FromResult(this.enrollments.AsEnumerable());

        public Task<IEnumerable<Enrollment>> FindAllByClass(string level, string module, string classroom) =>
            Task.FromResult(this.enrollments.Where(e => e.Level.Code == level && e.Module.Code == module && e.Class.Code == classroom));

        public Task<Enrollment?> FindByCpf(string cpf) => Task.FromResult(this.enrollments.SingleOrDefault(_ => _.Student.Cpf.Value == cpf));

        public Task<Enrollment> FindByCode(string code) =>
            Task.FromResult(this.enrollments.SingleOrDefault(_ => _.Code.Value == code) ?? throw new EnrollmentNotFoundException("Enrollment not found."));

        public Task Update(Enrollment enrollment) => Task.CompletedTask;

        public Task<int> Count() => Task.FromResult(this.enrollments.Count);
    }
}