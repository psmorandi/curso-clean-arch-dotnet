namespace CleanArch.School.Application.Domain.UseCase.Data.Mappers
{
    using System;
    using AutoMapper;
    using Entity;
    using Extensions;

    public class InvoiceOutputDataProfile : Profile
    {
        public InvoiceOutputDataProfile()
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