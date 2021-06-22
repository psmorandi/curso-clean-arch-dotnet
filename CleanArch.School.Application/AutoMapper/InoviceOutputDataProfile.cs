namespace CleanArch.School.Application.AutoMapper
{
    using CleanArch.School.Application.Extensions;
    using global::AutoMapper;
    using System;
    using Domain.Entity;

    public class InoviceOutputDataProfile : Profile
    {
        public InoviceOutputDataProfile()
        {
            this.CreateMap<Invoice, InvoiceOutputData>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.GetBalance()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest, destMember, context) => src.GetStatus(GetReferenceDateFrom(context))))
                .ForMember(dest => dest.Penalty, opt => opt.MapFrom((src, dest, destMember, context) => src.GetPenalty(GetReferenceDateFrom(context))))
                .ForMember(dest => dest.Interests, opt => opt.MapFrom((src, dest, destMember, context) => src.GetInterests(GetReferenceDateFrom(context))));
        }

        private static DateOnly GetReferenceDateFrom(ResolutionContext context) 
            => Convert.ToDateTime(context.Items["RefDate"].ToString()).ToDateOnly();
    }
}
