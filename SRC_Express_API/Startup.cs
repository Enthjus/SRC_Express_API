using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SRC_Express_API.Middlewares;
using SRC_Express_API.Models;
using SRC_Express_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRC_Express_API
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();
            var defaultConnection = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DatabaseContext>(options => options.UseLazyLoadingProxies().UseSqlServer(defaultConnection));
            services.AddHttpContextAccessor();

            services.AddScoped<AccountService, AccountServiceImpl>();
            services.AddScoped<LoginService, LoginServiceImpl>();
            services.AddScoped<CarsService, CarsServiceImpl>();
            services.AddScoped<TripsService, TripsServiceImpl>();
            services.AddScoped<PricePerKmService, PricePerKmServiceImpl>();
            services.AddScoped<PercentByAgeService, PercentByAgeServiceImpl>();
            services.AddScoped<RefundService, RefundServiceImpl>();
            services.AddScoped<ActiveTripService, ActiveTripServiceImpl>();
            services.AddScoped<TicketService, TicketServiceImpl>();
            services.AddScoped<DashboardService, DasboardServiceImpl>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseCors(builder => builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true)
               .AllowCredentials()
           );

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<CorsMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
