namespace CleanArch.School.Application.Adapter.Controller
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using TypeExtensions;
    using UseCase;
    using UseCase.Data;

    public class EnrollmentController : IEnrollmentController
    {
        private readonly IEnrollStudent enrollStudentUseCase;
        private readonly IGetEnrollment getEnrollmentUseCase;

        public EnrollmentController(IEnrollStudent enrollStudentUseCase, IGetEnrollment getEnrollmentUseCase)
        {
            this.enrollStudentUseCase = enrollStudentUseCase;
            this.getEnrollmentUseCase = getEnrollmentUseCase;
        }

        public async Task<HttpResponse> EnrollStudent(EnrollStudentInputData inputData, DateTime currentDate)
        {
            try
            {
                var enrollment = await this.enrollStudentUseCase.Execute(inputData, currentDate.ToDateOnly());
                return new Ok<EnrollStudentOutputData>(enrollment);
            }
            catch (Exception e)
            {
                return new ServerError(e);
            }
        }

        public async Task<HttpResponse> GetEnrollment(string code, DateTime currentDate)
        {
            try
            {
                var enrollment = await this.getEnrollmentUseCase.Execute(code, currentDate.ToDateOnly());
                return new Ok<GetEnrollmentOutputData>(enrollment);
            }
            catch (Exception e)
            {
                return new ServerError(e);
            }
        }
    }
}