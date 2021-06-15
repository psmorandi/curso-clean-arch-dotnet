namespace CleanArch.School.Application
{
    public class PayInvoice
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public PayInvoice(IRepositoryAbstractFactory repositoryFactory) 
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public void Execute(PayInvoiceInputData request)
        {
            var enrollment = this.enrollmentRepository.FindByCode(request.Code);
            enrollment.PayInvoice(request.Month, request.Year, request.Amount);
        }
    }
}