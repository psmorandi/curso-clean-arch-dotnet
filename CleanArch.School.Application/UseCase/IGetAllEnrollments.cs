namespace CleanArch.School.Application.UseCase
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;

    public interface IGetAllEnrollments
    {
        Task<IEnumerable<GetEnrollmentOutputData>> Execute(DateOnly currentDate);
    }
}