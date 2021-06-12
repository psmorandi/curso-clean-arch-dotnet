using CleanArch.School.Application;
using Xunit;

namespace CleanArch.School.UnitTests
{
    public class CancelEnrollmentTests : BaseEnrollmentTests
    {
        [Fact]
        public void Should_cancel_enrollment()
        {
            var enrollment = this.CreateRandomEnrollment();
            var enrollCode = enrollment.Code.Value;
            var cancelRequest = new CancelEnrollmentRequest { EnrollmentCode = enrollCode };
            var cancelEnrollment = new CancelEnrollment(this.enrollmentRepository);
            cancelEnrollment.Execute(cancelRequest);
            var cancelledEnrollment = this.GetEnrollment(enrollCode);
            Assert.True(cancelledEnrollment.Status == EnrollStatus.Cancelled);
        }
    }
}