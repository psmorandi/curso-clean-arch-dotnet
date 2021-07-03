namespace CleanArch.School.UnitTests
{
    using Application.UseCase;
    using AutoMapper;
    using Domain.Entity;
    using Xunit;

    public class AutoMapperTests
    {
        [Fact]
        public void Verify_AutoMapper_Configuration()
        {
            var configuration =
                new MapperConfiguration(cfg => cfg.AddMaps(typeof(Enrollment).Assembly,
                    typeof(Infrastructure.Database.Data.Enrollment).Assembly,
                    typeof(PayInvoice).Assembly));
            configuration.AssertConfigurationIsValid();
        }
    }
}