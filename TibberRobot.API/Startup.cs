using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;
using TibberRobot.Domain.Features.RobotMovement;
using TibberRobot.Repository;
using TibberRobot.Repository.Presistance;

namespace TibberRobot.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<RobotDbContext>(
                options => options
                    .UseNpgsql(Configuration.GetConnectionString("PostgreSQLDb"))
            );
            services.AddAutoMapper();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Tibber Robot", Version = "v1" });
            });

            services.AddScoped<IRobotMovementHandler, RobotMovementHandler>();
            services.AddScoped<IRobotMovementHelper, RobotMovementHelper>();
            services.AddScoped<IMovementRepository, MovementRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Tibber Robot");
            });

            app.UseMvc();

            app.Run(context =>
            {
                context.Response.Redirect("swagger/");
                return Task.CompletedTask;
            });
        }
    }
}
