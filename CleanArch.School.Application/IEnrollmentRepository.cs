namespace CleanArch.School.Application
{
    using System.Collections.Generic;

    public interface IEnrollmentRepository
    {
        void Save(Enrollment enrollment);

        ICollection<Enrollment> FindAllByClass(string level, string module, string classroom);

        Enrollment? FindByCpf(string cpf);

        Enrollment? FindByCode(string code);

        int Count();
    }
}