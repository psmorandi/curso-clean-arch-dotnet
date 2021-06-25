namespace CleanArch.School.Application.Domain.UseCase.Data.Mappers
{
    using AutoMapper;
    using Entity;

    public class EnrollStudentOutputDataProfile : Profile
    {
        public EnrollStudentOutputDataProfile()
        {
            this.CreateMap<Enrollment, EnrollStudentOutputData>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value));
        }
    }
}