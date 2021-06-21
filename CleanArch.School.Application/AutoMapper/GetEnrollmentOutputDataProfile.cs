namespace CleanArch.School.Application.AutoMapper
{
    using global::AutoMapper;

    public class GetEnrollmentOutputDataProfile : Profile
    {
        public GetEnrollmentOutputDataProfile()
        {
            this.CreateMap<Enrollment, GetEnrollmentOutputData>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name.Value))
                .ForMember(dest => dest.StudentCpf, opt => opt.MapFrom(src => src.Student.Cpf.Value))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.GetInvoiceBalance()));
        }
    }
}