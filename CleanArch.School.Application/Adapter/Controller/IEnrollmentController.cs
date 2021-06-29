namespace CleanArch.School.Application.Adapter.Controller
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Domain.UseCase.Data;

    public interface IEnrollmentController
    {
        Task<HttpResponse> EnrollStudent(EnrollStudentInputData inputData, DateTime currentDate);

        Task<HttpResponse> GetEnrollment(string code, DateTime currentDate);
    }
}