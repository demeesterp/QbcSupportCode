using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QbcBackend.Molecules.Repo;
using QbcServices;
using QbcWeb.Exceptions;

namespace QbcWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(o => o.Filters.Add(new GlobalExceptionFilter()));
            
            services.AddDbContext<QuantumbiochemistryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("molecules")), ServiceLifetime.Scoped);

            services.AddRepo();
            services.AddServices();
            services.AddValidation();

            services.AddMvc();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod());

            app.UseRouting();


            app.UseEndpoints(endpoints => endpoints.MapControllers());



        }
    }
}
