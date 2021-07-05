namespace CleanArch.School.API
{
    using System;
    using Filters;
    using Infrastructure.Database;
    using Injection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArch.School.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Any");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                options => { options.Filters.Add(typeof(ValidateModelAttribute)); });
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArch.School.API", Version = "v1" });
                    c.MapType<DateOnly>(() => new OpenApiSchema { Type = "string" });
                });
            services.AddDbContext<SchoolDbContext>(
                options =>
                    options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        "Any",
                        builder => { builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader(); });
                });

            services.AddUseCases();
            services.AddRepositories();
            services.ConfigureAutoMapper();
        }
    }
}