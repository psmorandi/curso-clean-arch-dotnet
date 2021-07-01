namespace CleanArch.School.UnitTests
{
    using AutoMapper;
    using Domain.Entity;
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