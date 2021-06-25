namespace CleanArch.School.Application.Adapter.Repository.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entity;
    using Domain.Repository;
    using Extensions;
    using Infra.Database;
    using Microsoft.EntityFrameworkCore;
    using Database = Entities;

    public class EnrollmentRepositoryDatabase : IEnrollmentRepository
    {
        private readonly SchoolDbContext dbContext;
        private readonly IMapper mapper;

        public EnrollmentRepositoryDatabase(SchoolDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Save(Enrollment enrollment)
        {
            this.dbContext.Students.Add(this.mapper.Map<Database.Student>(enrollment.Student));
            this.dbContext.Invoices.AddRange(this.mapper.Map<IEnumerable<Database.Invoice>>(enrollment.Invoices));
            foreach (var invoice in enrollment.Invoices)
            {
                var invoiceEvents = invoice.InvoiceEvents
                    .Select(
                        _ => new Database.InvoiceEvent
                             {
                                 Amount = _.Amount,
                                 Enrollment = enrollment.Code.Value,
                                 Month = invoice.DueDate.Month,
                                 Year = invoice.DueDate.Year,
                                 Type = _.Type.GetValue()
                             });
                this.dbContext.InvoiceEvents.AddRange(invoiceEvents);
            }

            this.dbContext.Enrollments.Add(this.mapper.Map<Database.Enrollment>(enrollment));
            await this.dbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<Enrollment>> FindAllByClass(string level, string module, string classroom) => throw new NotImplementedException();

        public Task<Enrollment?> FindByCpf(string cpf) => throw new NotImplementedException();

        public Task<Enrollment> FindByCode(string code) => throw new NotImplementedException();

        public async Task<int> Count() => await this.dbContext.Enrollments.CountAsync();
    }
}