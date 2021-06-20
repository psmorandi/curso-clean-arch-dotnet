namespace CleanArch.School.Application.AutoMapper
{
    using global::AutoMapper;

    public class EnrollStudentOutputDataProfile : Profile
    {
        public EnrollStudentOutputDataProfile()
        {
            this.CreateMap<Enrollment, EnrollStudentOutputData>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value));

            this.CreateMap<Invoice, InvoiceOutputData>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.GetBalance()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.GetStatus()));
        }
    }
}