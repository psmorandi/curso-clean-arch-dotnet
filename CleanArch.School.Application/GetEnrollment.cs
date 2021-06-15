namespace CleanArch.School.Application
{
    public class GetEnrollment
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public GetEnrollment(IRepositoryAbstractFactory repositoryFactory)
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public EnrollmentOutputData Execute(string code)
        {
            var enrollment = this.enrollmentRepository.FindByCode(code);
            return new EnrollmentOutputData
                   {
                       StudentName = enrollment.Student.Name.Value,
                       StudentCpf = enrollment.Student.Cpf.Value,
                       Code = enrollment.Code.Value,
                       InvoiceBalance = enrollment.GetInvoiceBalance()
                   };
        }
    }
}