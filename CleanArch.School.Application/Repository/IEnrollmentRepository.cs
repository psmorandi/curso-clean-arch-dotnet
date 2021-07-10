namespace CleanArch.School.Application.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Entity;

    public interface IEnrollmentRepository
    {
        Task Save(Enrollment enrollment);

        Task<IEnumerable<Enrollment>> GetAll();

        Task<IEnumerable<Enrollment>> FindAllByClass(string level, string module, string classroom);

        Task<Enrollment?> FindByCpf(string cpf);

        Task<Enrollment> FindByCode(string code);

        Task Update(Enrollment enrollment);

        Task<int> Count();
    }
}