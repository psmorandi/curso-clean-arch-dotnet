namespace CleanArch.School.Application.Domain.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entity;

    public interface IEnrollmentRepository
    {
        Task Save(Enrollment enrollment);

        Task<IEnumerable<Enrollment>> FindAllByClass(string level, string module, string classroom);

        Task<Enrollment?> FindByCpf(string cpf);

        Task<Enrollment> FindByCode(string code);

        Task<int> Count();
    }
}