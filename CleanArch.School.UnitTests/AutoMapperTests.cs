namespace CleanArch.School.UnitTests
{
    using Application.Domain.Entity;
    using AutoMapper;
    using Xunit;

    public class AutoMapperTests
    {
        [Fact]
        public void Verify_AutoMapper_Configuration()
        {
            var configuration =
                new MapperConfiguration(cfg => cfg.AddMaps(typeof(Enrollment).Assembly));
            configuration.AssertConfigurationIsValid();
        }
    }
}