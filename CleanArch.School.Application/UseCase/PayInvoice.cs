namespace CleanArch.School.Application.UseCase
{
    using System.Threading.Tasks;
    using Data;
    using Factory;
    using Repository;

    public class PayInvoice : IPayInvoice
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public PayInvoice(IRepositoryAbstractFactory repositoryFactory)
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public async Task Execute(PayInvoiceInputData request)
        {
            var enrollment = await this.enrollmentRepository.FindByCode(request.Code);
            enrollment.PayInvoice(request.Month, request.Year, request.Amount, request.PaymentDate);
            await this.enrollmentRepository.Update(enrollment);
        }
    }
}