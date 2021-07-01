namespace CleanArch.School.Application.UseCase.Extensions
{
    using System;
    using AutoMapper;

    public static class MapperExtensions
    {
        public static TDestination Map<TDestination>(this IMapper mapper, object source, DateOnly refDate)
            => mapper.Map<TDestination>(source, opt => opt.Items["RefDate"] = refDate);
    }
}