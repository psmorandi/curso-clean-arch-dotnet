namespace CleanArch.School.Application.AutoMapper
{
    using Domain.Entity;
    using global::AutoMapper;

    public class EnrollStudentOutputDataProfile : Profile
    {
        public EnrollStudentOutputDataProfile()
        {
            this.CreateMap<Enrollment, EnrollStudentOutputData>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value));
        }
    }
}