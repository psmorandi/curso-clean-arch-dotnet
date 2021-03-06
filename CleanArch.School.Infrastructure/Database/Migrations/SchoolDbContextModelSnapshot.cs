// <auto-generated />
namespace CleanArch.School.Infrastructure.Database.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    [DbContext(typeof(SchoolDbContext))]
    partial class SchoolDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CleanArch.School.Application.Adapter.Repository.Database.Data.Classroom", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Level")
                        .HasColumnType("text")
                        .HasColumnName("level");

                    b.Property<string>("Module")
                        .HasColumnType("text")
                        .HasColumnName("module");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer")
                        .HasColumnName("capacity");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("start_date");

                    b.HasKey("Code", "Level", "Module")
                        .HasName("pk_classrooms");

                    b.ToTable("classrooms");
                });

            modelBuilder.Entity("CleanArch.School.Application.Adapter.Repository.Database.Data.Enrollment", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Classroom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("classroom");

                    b.Property<int>("Installments")
                        .HasColumnType("integer")
                        .HasColumnName("installments");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("issue_date");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("level");

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("module");

                    b.Property<int>("Sequence")
                        .HasColumnType("integer")
                        .HasColumnName("sequence");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("Student")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("student");

                    b.HasKey("Code")
                        .HasName("pk_enrollments");

                    b.ToTable("enrollments");
                });

            modelBuilder.Entity("CleanArch.School.Application.Adapter.Repository.Database.Data.Invoice", b =>
                {
                    b.Property<string>("Enrollment")
                        .HasColumnType("text")
                        .HasColumnName("enrollment");

                    b.Property<int>("Month")
                        .HasColumnType("integer")
                        .HasColumnName("month");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("due_date");

                    b.HasKey("Enrollment", "Month", "Year")
                        .HasName("pk_invoices");

                    b.ToTable("invoices");
                });

            modelBuilder.Entity("CleanArch.School.Application.Adapter.Repository.Database.Data.InvoiceEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<string>("Enrollment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("enrollment");

                    b.Property<int>("Month")
                        .HasColumnType("integer")
                        .HasColumnName("month");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("pk_invoice_events");

                    b.ToTable("invoice_events");
                });

            modelBuilder.Entity("CleanArch.School.Application.Adapter.Repository.Database.Data.Level", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("Code")
                        .HasName("pk_levels");

                    b.ToTable("levels");
                });

            modelBuilder.Entity("CleanArch.School.Application.Adapter.Repository.Database.Data.Module", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Level")
                        .HasColumnType("text")
                        .HasColumnName("level");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("MinimumAge")
                        .HasColumnType("integer")
                        .HasColumnName("minimum_age");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.HasKey("Code", "Level")
                        .HasName("pk_modules");

                    b.ToTable("modules");
                });

            modelBuilder.Entity("CleanArch.School.Application.Adapter.Repository.Database.Data.Student", b =>
                {
                    b.Property<string>("Cpf")
                        .HasColumnType("text")
                        .HasColumnName("cpf");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Cpf")
                        .HasName("pk_students");

                    b.ToTable("students");
                });
#pragma warning restore 612, 618
        }
    }
}
