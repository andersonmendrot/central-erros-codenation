using CentralErros.Infrastructure;
using CentralErros.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using System;
using CentralErros.Infrastructure.Repositories;
using CentralErros.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using IAuthorizationService = CentralErros.Domain.Repositories.IAuthorizationService;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace CentralErros.API
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

            services.AddHttpContextAccessor(); // here
            services.AddSingleton<IAuthenticationService, JwtIdentityAuthenticationService>();
            services.AddSingleton<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<ILoggedUserService, IdentityLoggedUserService>(); // here

            var token = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(Configuration.GetSection(typeof(TokenConfiguration).Name)).Configure(token);
            services.AddSingleton(token);

            services.AddControllers();

            var signing = new SigningConfiguration();
            services.AddSingleton(signing);

            services.AddDbContext<CentralErrosContext>();

            services.AddAuthentication(opt => // here
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters.IssuerSigningKey = signing.Key;
                x.TokenValidationParameters.ValidAudience = token.ValidAudience;
                x.TokenValidationParameters.ValidIssuer = token.ValidIssuer;
                x.TokenValidationParameters.ValidateIssuerSigningKey = token.ValidateIssuerSigningKey;
                x.TokenValidationParameters.ValidateLifetime = token.ValidateLifetime;
                x.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth => // here
            {
                auth.AddPolicy(TokenConfiguration.Policy, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            services
           .AddMvc()
           .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
           .AddJsonOptions(opt =>
           {
               opt.JsonSerializerOptions.IgnoreNullValues = true;
           });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IApplicationLayerRepository, ApplicationLayerRepository>();
            services.AddScoped<IEnvironmentRepository, EnvironmentRepository>();
            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}