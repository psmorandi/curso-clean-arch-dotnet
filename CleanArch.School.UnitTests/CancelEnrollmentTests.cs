namespace CleanArch.School.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using Application.Domain.Entity;
    using Application.Domain.UseCase;
    using Application.Extensions;
    using Xunit;

    public class CancelEnrollmentTests : BaseEnrollmentTests
    {
        [Fact]
        public async Task Should_cancel_enrollment()
        {
            var refDate = DateTime.UtcNow.ToDateOnly();
            var enrollment = await this.CreateRandomEnrollment(refDate);
            var enrollCode = enrollment.Code;
            var cancelEnrollment = new CancelEnrollment(this.repositoryFactory);
            await cancelEnrollment.Execute(enrollCode);
            var cancelledEnrollment = await this.GetEnrollment(enrollCode, refDate);
            Assert.True(cancelledEnrollment.Status == EnrollStatus.Cancelled);
        }
    }
}