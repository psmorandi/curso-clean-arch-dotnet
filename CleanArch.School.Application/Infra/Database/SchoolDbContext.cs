#pragma warning disable 8618
namespace CleanArch.School.Application.Infra.Database
{
    using Adapter.Repository.Database.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options) { }

        public DbSet<Level> Levels { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceEvent> InvoiceEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new LevelConfiguration());
            builder.ApplyConfiguration(new ModuleConfiguration());
            builder.ApplyConfiguration(new ClassroomConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new EnrollmentConfiguration());
            builder.ApplyConfiguration(new InvoiceConfiguration());
            builder.ApplyConfiguration(new InvoiceEventConfiguration());
        }

        private class LevelConfiguration : IEntityTypeConfiguration<Level>
        {
            public void Configure(EntityTypeBuilder<Level> builder)
            {
                builder.HasKey(_ => _.Code);
                builder.Property(_ => _.Description).IsRequired();
            }
        }

        private class ModuleConfiguration : IEntityTypeConfiguration<Module>
        {
            public void Configure(EntityTypeBuilder<Module> builder)
            {
                builder.HasKey(_ => new { _.Code, _.Level });
                builder.Property(_ => _.Description).IsRequired();
                builder.Property(_ => _.MinimumAge).IsRequired();
                builder.Property(_ => _.Price).IsRequired();
            }
        }

        private class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
        {
            public void Configure(EntityTypeBuilder<Classroom> builder)
            {
                builder.HasKey(_ => new { _.Code, _.Level, _.Module });
                builder.Property(_ => _.Capacity).IsRequired();
                builder.Property(_ => _.StartDate).IsRequired();
                builder.Property(_ => _.EndDate).IsRequired();
            }
        }

        private class StudentConfiguration : IEntityTypeConfiguration<Student>
        {
            public void Configure(EntityTypeBuilder<Student> builder)
            {
                builder.HasKey(_ => _.Cpf);
                builder.Property(_ => _.Name).IsRequired();
                builder.Property(_ => _.BirthDate).IsRequired();
            }
        }

        private class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
        {
            public void Configure(EntityTypeBuilder<Enrollment> builder)
            {
                builder.HasKey(_ => _.Code);
                builder.Property(_ => _.Level).IsRequired();
                builder.Property(_ => _.Module).IsRequired();
                builder.Property(_ => _.Classroom).IsRequired();
                builder.Property(_ => _.Student).IsRequired();
                builder.Property(_ => _.Installments).IsRequired();
                builder.Property(_ => _.IssueDate).IsRequired();
                builder.Property(_ => _.Status).IsRequired();
                builder.Property(_ => _.Sequence).IsRequired();
            }
        }

        private class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
        {
            public void Configure(EntityTypeBuilder<Invoice> builder)
            {
                builder.HasKey(_ => new { _.Enrollment, _.Month, _.Year });
                builder.Property(_ => _.DueDate).IsRequired();
                builder.Property(_ => _.Amount).IsRequired();
            }
        }

        private class InvoiceEventConfiguration : IEntityTypeConfiguration<InvoiceEvent>
        {
            public void Configure(EntityTypeBuilder<InvoiceEvent> builder)
            {
                builder.Property(_ => _.Enrollment).IsRequired();
                builder.Property(_ => _.Month).IsRequired();
                builder.Property(_ => _.Year).IsRequired();
                builder.Property(_ => _.Type).IsRequired();
                builder.Property(_ => _.Amount).IsRequired();
            }
        }
    }
}