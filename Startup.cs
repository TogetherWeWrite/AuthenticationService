using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AuthenticationService.Interfaces;
using AuthenticationService.Logic;
using AuthenticationService.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using AuthenticationService.Services;

namespace AuthenticationService
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
            services.AddControllers();
            #region database injection 
            services.AddDbContext<AuthenticationContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));


            services.AddTransient<AuthenticationContext, AuthenticationContext>();
            #endregion
            #region Repository injection
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            services.AddTransient<IRegister, AuthenticationRepository>();
            services.AddTransient<ILogin, AuthenticationRepository>();
            #endregion
            #region Services injection
            services.AddTransient<IEncryptionService, EncryptionService>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //TODO: USE HTTPS think about this reverse proxy.
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
