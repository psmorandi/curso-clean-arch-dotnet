namespace CleanArch.School.Application.Extensions
{
    using System;
    using global::AutoMapper;

    public static class MapperExtensions
    {
        public static TDestination Map<TDestination>(this IMapper mapper, object source, DateOnly refDate)
            => mapper.Map<TDestination>(source, opt => opt.Items["RefDate"] = refDate);
    }
}