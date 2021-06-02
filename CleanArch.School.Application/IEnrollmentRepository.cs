namespace CleanArch.School.Application
{
    using System.Collections.Generic;

    public interface IEnrollmentRepository
    {
        void Save(Enrollment enrollment);

        ICollection<Enrollment> FindAllByClass(string level, string module, string @class);

        Enrollment? FindByCpf(string cpf);
    }
}