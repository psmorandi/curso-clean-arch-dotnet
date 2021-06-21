namespace CleanArch.School.Application.AutoMapper
{
    using CleanArch.School.Application.Extensions;
    using global::AutoMapper;
    using System;

    public class InoviceOutputDataProfile : Profile
    {
        public InoviceOutputDataProfile()
        {
            this.CreateMap<Invoice, InvoiceOutputData>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.GetBalance()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest, destMember, context) => src.GetStatus(Convert.ToDateTime(context.Items["RefDate"].ToString()).ToDateOnly())))
                .ForMember(dest => dest.Penalty, opt => opt.MapFrom((src, dest, destMember, context) => src.GetPenalty(Convert.ToDateTime(context.Items["RefDate"].ToString()).ToDateOnly())))
                .ForMember(dest => dest.Interests, opt => opt.MapFrom((src, dest, destMember, context) => src.GetInterests(Convert.ToDateTime(context.Items["RefDate"].ToString()).ToDateOnly())));
        }
    }
}
