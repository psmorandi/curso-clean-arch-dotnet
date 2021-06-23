﻿namespace CleanArch.School.Application.Domain.UseCase
{
    using System.Threading.Tasks;
    using Factory;
    using Repository;

    public class PayInvoice
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public PayInvoice(IRepositoryAbstractFactory repositoryFactory)
            => this.enrollmentRepository = repositoryFactory.CreateEnrollmentRepository();

        public async Task Execute(PayInvoiceInputData request)
        {
            var enrollment = await this.enrollmentRepository.FindByCode(request.Code);
            enrollment.PayInvoice(request.Month, request.Year, request.Amount, request.PaymentDate);
        }
    }
}