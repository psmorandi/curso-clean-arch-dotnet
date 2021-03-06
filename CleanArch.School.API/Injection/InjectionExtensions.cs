namespace CleanArch.School.API.Injection
{
    using Application.Factory;
    using Application.UseCase;
    using Domain.Entity;
    using Infrastructure.Factory;
    using Microsoft.Extensions.DependencyInjection;
    using WebApp.Shared.Data;

    public static class InjectionExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IEnrollStudent, EnrollStudent>();
            services.AddTransient<IGetEnrollment, GetEnrollment>();
            services.AddTransient<IGetAllEnrollments, GetAllEnrollments>();
            services.AddTransient<IPayInvoice, PayInvoice>();
            services.AddTransient<ICancelEnrollment, CancelEnrollment>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(Enrollment).Assembly,
                typeof(Infrastructure.Database.Data.Enrollment).Assembly,
                typeof(PayInvoice).Assembly,
                typeof(EnrollmentResponse).Assembly);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryAbstractFactory, RepositoryDatabaseAbstractFactory>();
        }
    }
}