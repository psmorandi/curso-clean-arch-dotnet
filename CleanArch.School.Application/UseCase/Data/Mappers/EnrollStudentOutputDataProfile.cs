namespace CleanArch.School.Application.UseCase.Data.Mappers
{
    using AutoMapper;
    using Domain.Entity;

    public class EnrollStudentOutputDataProfile : Profile
    {
        public EnrollStudentOutputDataProfile()
        {
            this.CreateMap<Enrollment, EnrollStudentOutputData>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value));
        }
    }
}