namespace CleanArch.School.Application.UseCase
{
    using System;
    using System.Threading.Tasks;
    using Data;

    public interface IEnrollStudent
    {
        Task<EnrollStudentOutputData> Execute(EnrollStudentInputData inputData, DateOnly currentDate);
    }
}