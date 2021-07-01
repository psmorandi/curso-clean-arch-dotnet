namespace CleanArch.School.Application.UseCase
{
    using System;
    using System.Threading.Tasks;
    using Data;

    public interface IGetEnrollment
    {
        Task<GetEnrollmentOutputData> Execute(string code, DateOnly currentDate);
    }
}