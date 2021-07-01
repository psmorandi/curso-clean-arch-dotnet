namespace CleanArch.School.Infrastructure.Database.Data.Mappers
{
    using System;
    using AutoMapper;
    using Domain.Entity;
    using Extensions;
    using TypeExtensions;
    using Classroom = Data.Classroom;
    using Enrollment = Data.Enrollment;
    using Invoice = Data.Invoice;
    using Level = Data.Level;
    using Module = Data.Module;
    using Student = Data.Student;

    public class EntitiesMapperProfile : Profile
    {
        public EntitiesMapperProfile()
        {
            this.ConfigureDomainEntityMappingToDatabaseEntity();
            this.ConfigureDatabaseEntityToDomainEntity();
        }

        private void ConfigureDomainEntityMappingToDatabaseEntity()
        {
            this.CreateMap<Domain.Entity.Level, Level>();
            this.CreateMap<Domain.Entity.Module, Module>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToInt32(src.Price)));
            this.CreateMap<Domain.Entity.Classroom, Classroom>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Period.StartDate.ToDateTime()))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Period.EndDate.ToDateTime()));
            this.CreateMap<Domain.Entity.Student, Student>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Birthday.ToDateTime()))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                ;
            this.CreateMap<Domain.Entity.Invoice, Invoice>()
                .ForMember(dest => dest.Enrollment, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate.ToDateTime()))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.DueDate.Month))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.DueDate.Year));
            this.CreateMap<Domain.Entity.Enrollment, Enrollment>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.Code))
                .ForMember(dest => dest.Module, opt => opt.MapFrom(src => src.Module.Code))
                .ForMember(dest => dest.Classroom, opt => opt.MapFrom(src => src.Class.Code))
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student.Cpf.Value))
                .ForMember(dest => dest.Installments, opt => opt.MapFrom(src => src.Invoices.Count))
                .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src.IssueDate.ToDateTime()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetValue()))
                .ForMember(dest => dest.Sequence, opt => opt.MapFrom(src => src.Sequence));
        }

        private void ConfigureDatabaseEntityToDomainEntity()
        {
            this.CreateMap<Level, Domain.Entity.Level>()
                .ConstructUsing((src, _) => new Domain.Entity.Level(src.Code, src.Description));
            this.CreateMap<Module, Domain.Entity.Module>()
                .ConstructUsing(
                    (src, _)
                        => new Domain.Entity.Module(src.Level, src.Code, src.Description, src.MinimumAge, src.Price));
            this.CreateMap<Classroom, Domain.Entity.Classroom>()
                .ConstructUsing(
                    (src, _)
                        => new Domain.Entity.Classroom(src.Level, src.Module, src.Code, src.Capacity, src.StartDate.ToDateOnly(), src.EndDate.ToDateOnly()));
            this.CreateMap<Student, Domain.Entity.Student>()
                .ConstructUsing((src, _) => new Domain.Entity.Student(src.Name, src.Cpf, src.BirthDate));
        }
    }
}