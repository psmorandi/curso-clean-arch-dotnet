using AutoMapper;
using System;

namespace CleanArch.School.Application.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination Map<TDestination>(this IMapper mapper, object source, DateOnly refDate) 
            => mapper.Map<TDestination>(source, opt => opt.Items["RefDate"] = refDate);
    }
}
