namespace CleanArch.School.Application.AutoMapper
{
    using global::AutoMapper;

    public class EnrollmentOutputDataProfile : Profile
    {
        public EnrollmentOutputDataProfile()
        {
            this.CreateMap<Enrollment, EnrollmentOutputData>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name.Value))
                .ForMember(dest => dest.StudentCpf, opt => opt.MapFrom(src => src.Student.Cpf.Value))
                .ForMember(dest => dest.InvoiceBalance, opt => opt.MapFrom(src => src.GetInvoiceBalance()));
        }
    }
}