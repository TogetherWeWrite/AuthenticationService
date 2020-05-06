using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AuthenticationService.Interfaces;
using AuthenticationService.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using AuthenticationService.Services;
using AuthenticationService.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AuthenticationService.Services.Interfaces;

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
            services.AddCors();
            #region jwt
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            });
            #endregion
            #region database injection 
            services.AddDbContext<AuthenticationContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));


            services.AddTransient<AuthenticationContext, AuthenticationContext>();
            #endregion
            #region Repository injection
            services.AddTransient<IAccountRepository, AccountRepository>();
            #endregion
            #region Services injection
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthenticationService, AuthenticationServices>();
            services.AddTransient<IRegister, AuthenticationServices>();
            services.AddTransient<ILogin, AuthenticationServices>();
            services.AddTransient<IAuthenticationService, AuthenticationServices>();
            services.AddTransient<IAccountservice, AccountService>();
            #endregion
            services.AddControllers();

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

            app.UseCors(x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
