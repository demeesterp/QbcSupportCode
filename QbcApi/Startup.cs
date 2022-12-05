using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using QbcApi.Exceptions;
using QbcBackend.Molecules.Repo;
using QbcServices;
using System.IO;

namespace QbcApi
{
    public class Startup
    {

        #region properites

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion


        public Startup(IWebHostEnvironment env)
        {
            var path = env.ContentRootPath;
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuantumbiochemistryContext>(options =>
                                    options.UseSqlServer(Configuration.GetConnectionString("molecules")), ServiceLifetime.Scoped);

            services.AddRepo();
            services.AddServices();
            services.AddValidation();

            services.AddCors();

            services.AddMvc().AddMvcOptions(o => o.Filters.Add(new GlobalExceptionFilter()))
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Qbc", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "QbcApi.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod());

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                if (env.IsDevelopment())
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Qbc");
                }
                else
                {
                    c.SwaggerEndpoint("/qbcapi/swagger/v1/swagger.json", "Qbc");
                }
            });


        }
    }
}
