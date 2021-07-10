namespace CleanArch.School.WebApp.Shared.Data.Mappers
{
    using Application.UseCase.Data;
    using AutoMapper;

    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            this.CreateMap<EnrollStudentRequest, EnrollStudentInputData>();
            this.MapResponses();
        }

        public void MapResponses()
        {
            this.CreateMap<InvoiceOutputData, InvoiceResponse>();
            this.CreateMap<EnrollStudentOutputData, EnrollStudentResponse>();
            this.CreateMap<GetEnrollmentOutputData, EnrollmentResponse>();
        }
    }
}