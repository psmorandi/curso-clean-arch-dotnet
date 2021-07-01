namespace CleanArch.School.Infrastructure.Repository.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Repository;
    using AutoMapper;
    using Domain.Entity;
    using Infrastructure.Database;
    using Infrastructure.Database.Data.Extensions;
    using Microsoft.EntityFrameworkCore;
    using TypeExtensions;
    using Database = Infrastructure.Database.Data;

    public class EnrollmentRepositoryDatabase : IEnrollmentRepository
    {
        private readonly SchoolDbContext dbContext;
        private readonly IMapper mapper;

        public EnrollmentRepositoryDatabase(SchoolDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> Count() => await this.dbContext.Enrollments.CountAsync();

        public async Task Save(Enrollment enrollment)
        {
            this.dbContext.Students.Add(this.mapper.Map<Database.Student>(enrollment.Student));
            this.dbContext.Invoices.AddRange(this.mapper.Map<IEnumerable<Database.Invoice>>(enrollment.Invoices));
            this.dbContext.Enrollments.Add(this.mapper.Map<Database.Enrollment>(enrollment));
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Enrollment>> FindAllByClass(string level, string module, string classroom)
        {
            var dbEnrollments = await this.dbContext.Enrollments.AsQueryable()
                                    .Where(
                                        e => e.Level == level &&
                                             e.Module == module &&
                                             e.Classroom == classroom)
                                    .ToListAsync();
            var enrollments = new List<Enrollment>();
            foreach (var enrollment in dbEnrollments) enrollments.Add((await this.CreateEnrollmentAsync(enrollment))!);

            return enrollments;
        }

        public async Task<Enrollment?> FindByCpf(string cpf)
        {
            var enrollment = await this.dbContext.Enrollments
                                 .AsQueryable()
                                 .AsNoTracking()
                                 .SingleOrDefaultAsync(e => e.Student == cpf);
            if (enrollment == null) return null;
            return await this.CreateEnrollmentAsync(enrollment);
        }

        public async Task<Enrollment> FindByCode(string code)
        {
            var enrollment = await this.dbContext.Enrollments
                                 .AsQueryable()
                                 .AsNoTracking()
                                 .SingleOrDefaultAsync(e => e.Code == code) ?? throw new Exception("Enrollment not found.");
            if (enrollment == null) throw new Exception("Enrollment not found.");
            return await this.CreateEnrollmentAsync(enrollment) ?? throw new Exception("Enrollment not found.");
        }

        public async Task Update(Enrollment enrollment)
        {
            var enrollmentData = await this.dbContext.Enrollments.FindAsync(enrollment.Code.Value);
            enrollmentData.Installments = enrollment.Invoices.Count;
            enrollmentData.Status = enrollment.Status.GetValue();
            foreach (var invoice in enrollment.Invoices)
            {
                var code = invoice.Code;
                var month = invoice.DueDate.Month;
                var year = invoice.DueDate.Year;
                var invoiceData = await this.dbContext.Invoices.FindAsync(code, month, year);
                invoiceData.Amount = invoice.Amount;
                await this.RemoveAllInvoicesEvents(code, month, year);
                var invoiceEvents = invoice.InvoiceEvents
                    .Select(
                        _ => new Database.InvoiceEvent
                             {
                                 Amount = _.Amount,
                                 Enrollment = code,
                                 Month = month,
                                 Year = year,
                                 Type = _.Type.GetValue()
                             });
                this.dbContext.InvoiceEvents.AddRange(invoiceEvents);
            }

            await this.dbContext.SaveChangesAsync();
        }

        private Task<Student> GetStudentAsync(string cpf)
        {
            return this.dbContext.Students.AsQueryable()
                .AsNoTracking()
                .Where(e => e.Cpf == cpf)
                .Select(e => new Student(e.Name, e.Cpf, e.BirthDate))
                .SingleAsync();
        }

        private Task<Level> GetLevelAsync(string level)
        {
            return this.dbContext.Levels.AsQueryable()
                .AsNoTracking()
                .Where(l => l.Code == level)
                .Select(l => new Level(l.Code, l.Description))
                .SingleAsync();
        }

        private Task<Module> GetModuleAsync(string module, string level)
        {
            return this.dbContext.Modules.AsQueryable()
                .AsNoTracking()
                .Where(m => m.Code == module && m.Level == level)
                .Select(m => new Module(m.Level, m.Code, m.Description, m.MinimumAge, m.Price))
                .SingleAsync();
        }

        private Task<Classroom> GetClassroomAsync(string classroom)
        {
            return this.dbContext.Classrooms.AsQueryable()
                .AsNoTracking()
                .Where(c => c.Code == classroom)
                .Select(c => new Classroom(c.Level, c.Module, c.Code, c.Capacity, c.StartDate.ToDateOnly(), c.EndDate.ToDateOnly()))
                .SingleAsync();
        }

        private Task<List<Database.Invoice>> GetInvoicesAsync(Database.Enrollment enrollment)
        {
            return this.dbContext.Invoices.AsQueryable()
                .AsNoTracking()
                .Where(i => i.Enrollment == enrollment.Code)
                .OrderBy(i => i.Year)
                .ThenBy(i => i.Month)
                .ToListAsync();
        }

        private Task<List<Database.InvoiceEvent>> GetInvoicesAsync(Database.Invoice invoice)
        {
            return this.dbContext.InvoiceEvents.AsQueryable()
                .AsNoTracking()
                .Where(
                    e => e.Enrollment == invoice.Enrollment &&
                         e.Month == invoice.Month &&
                         e.Year == invoice.Year)
                .ToListAsync();
        }

        private async Task<Enrollment?> CreateEnrollmentAsync(Database.Enrollment enrollment)
        {
            var student = await this.GetStudentAsync(enrollment.Student);
            var level = await this.GetLevelAsync(enrollment.Level);
            var module = await this.GetModuleAsync(enrollment.Module, enrollment.Level);
            var classroom = await this.GetClassroomAsync(enrollment.Classroom);
            var dbInvoices = await this.GetInvoicesAsync(enrollment);

            var invoices = new List<Invoice>();

            foreach (var invoice in dbInvoices)
            {
                var dbEvents = await this.GetInvoicesAsync(invoice);
                var invoiceEvents = dbEvents
                    .ConvertAll<InvoiceEvent>(
                        i => i.Type.ToInvoiceEventType() switch
                        {
                            InvoiceEventType.Payment => new InvoicePaidEvent(i.Amount),
                            InvoiceEventType.Penalty => new InvoicePenaltyEvent(i.Amount),
                            InvoiceEventType.Interests => new InvoiceInterestsEvent(i.Amount),
                            _ => throw new Exception("Invalid invoice event type.")
                        });
                invoices.Add(
                    Invoice.Load(
                        invoice.Enrollment,
                        invoice.DueDate.Day,
                        invoice.Month,
                        invoice.Year,
                        invoice.Amount,
                        invoiceEvents));
            }

            return Enrollment.Load(
                enrollment.Sequence,
                student,
                classroom,
                level,
                module,
                enrollment.IssueDate.ToDateOnly(),
                invoices,
                enrollment.Status.ToEnrollStatus()
            );
        }

        private async Task RemoveAllInvoicesEvents(string code, int month, int year)
        {
            var invoiceEventsData =
                await this.dbContext.InvoiceEvents.AsQueryable()
                    .Where(e => e.Enrollment == code && e.Month == month && e.Year == year)
                    .ToListAsync();
            this.dbContext.InvoiceEvents.RemoveRange(invoiceEventsData);
        }
    }
}