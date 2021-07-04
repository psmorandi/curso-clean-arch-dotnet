namespace CleanArch.School.API.Injection
{
    using Application.Factory;
    using Application.UseCase;
    using CleanArch.School.API.Controllers;
    using Domain.Entity;
    using Infrastructure.Factory;
    using Microsoft.Extensions.DependencyInjection;

    public static class InjectionExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IEnrollStudent, EnrollStudent>();
            services.AddTransient<IGetEnrollment, GetEnrollment>();
            services.AddTransient<IPayInvoice, PayInvoice>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(Enrollment).Assembly,
                typeof(Infrastructure.Database.Data.Enrollment).Assembly,
                typeof(PayInvoice).Assembly,
                typeof(EnrollmentsController).Assembly);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryAbstractFactory, RepositoryDatabaseAbstractFactory>();
        }
    }
}