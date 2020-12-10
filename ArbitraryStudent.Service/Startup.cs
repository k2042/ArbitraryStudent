using ArbitraryStudent.Service.Controllers.Model;
using ArbitraryStudent.Service.Controllers.Model.Validation;
using ArbitraryStudent.Service.Db;
using ArbitraryStudent.Service.Services;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Newtonsoft.Json;

namespace ArbitraryStudent.Service
{
    /// <summary>
    /// There are almost none comments in the project as it's quite elementary and
    /// not that different from a basic asp.net core template. The comment here 
    /// proves that I can comment.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ArbitraryDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("ArbitraryDbContext"));
            }, ServiceLifetime.Scoped);

            services.AddScoped<StudentService>();
            services.AddScoped<DictionaryService>();

            services.AddSingleton<IValidator<PostStudent>, PostStudentValidator<PostStudent>>();
            services.AddSingleton<IValidator<PutStudent>, PutStudentValidator<PutStudent>>();

            services.AddMvc();
            services.AddSwaggerGen();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values
                        .SelectMany(entry => entry.Errors.Select(p => p.ErrorMessage))
                        .ToList();

                    return new BadRequestObjectResult(new { errors });
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(config =>
            {
                config.AllowAnyOrigin();
                config.AllowAnyMethod();
                config.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            // Register exception-hanling middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
