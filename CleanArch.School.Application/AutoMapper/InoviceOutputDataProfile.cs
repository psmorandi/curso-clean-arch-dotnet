namespace CleanArch.School.Application.AutoMapper
{
    using global::AutoMapper;

    public class InoviceOutputDataProfile : Profile
    {
        public InoviceOutputDataProfile()
        {
            this.CreateMap<Invoice, InvoiceOutputData>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.GetBalance()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.GetStatus()));
        }
    }
}
