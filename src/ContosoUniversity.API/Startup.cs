using AutoMapper;
using ContosoUniversity.API.Core;
using ContosoUniversity.API.Filters;
using ContosoUniversity.Data;
using ContosoUniversity.Data.Abstract;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Model.ViewModels.Mapping;
using ContosoUniversity.Model.ViewModels.Validations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace ContosoUniversity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
            })
            
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StudentViewModelValidator>());
        

            //Add database configurations
            services.AddDbContext<ContosoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ContosoUniversity"),
                b => b.MigrationsAssembly("ContosoUniversity.Data"));

            });

            //CORS - not needed as for now
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyOrigin() //Don't forget the slash if specified!
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials());
            //});


        
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "ContosoUniversity.API";
                });

            //Automapper   
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });


            //Repositories
            services.AddScoped<IStudentsRepository, StudentsRepository>();
            services.AddScoped<ICoursesRepository, CoursesRepository>();
            services.AddScoped<IInstructorsRepository, InstructorsRepository>();
            services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {

           app.UseStaticFiles();

         //app.UseCors("CorsPolicy");



          app.UseExceptionHandler(builder =>
            {
                builder.Run(
                async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                    }
                });
            });
        
          app.UseAuthentication();

          app.UseMvcWithDefaultRoute();

        }

    }
}
