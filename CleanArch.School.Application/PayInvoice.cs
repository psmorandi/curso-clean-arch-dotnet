namespace CleanArch.School.Application
{
    public class PayInvoice
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public PayInvoice(IEnrollmentRepository enrollmentRepository) => this.enrollmentRepository = enrollmentRepository;

        public void Execute(PayInvoiceRequest request)
        {
            var enrollment = this.enrollmentRepository.FindByCode(request.Code);
            var invoice = enrollment.GetInvoice(request.Month, request.Year);
            invoice.Pay(request.Amount);
        }
    }
}