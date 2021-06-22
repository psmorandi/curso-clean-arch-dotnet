namespace CleanArch.School.Application.Domain.Repository
{
    using System.Collections.Generic;
    using Entity;

    public interface IEnrollmentRepository
    {
        void Save(Enrollment enrollment);

        ICollection<Enrollment> FindAllByClass(string level, string module, string classroom);

        Enrollment? FindByCpf(string cpf);

        Enrollment FindByCode(string code);

        int Count();
    }
}