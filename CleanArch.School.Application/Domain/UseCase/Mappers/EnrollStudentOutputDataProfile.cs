namespace CleanArch.School.Application.Domain.UseCase.Mappers
{
    using Entity;
    using global::AutoMapper;
    using UseCase;

    public class EnrollStudentOutputDataProfile : Profile
    {
        public EnrollStudentOutputDataProfile()
        {
            this.CreateMap<Enrollment, EnrollStudentOutputData>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value));
        }
    }
}