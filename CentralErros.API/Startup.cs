using System;
using CentralErros.Infrastructure;
using CentralErros.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CentralErros.Infrastructure.Repositories;
using CentralErros.Domain.Models;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

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
            services.AddHttpContextAccessor();
              
            services.AddDbContext<CentralErrosContext>();

            services.AddCors();

            services.AddScoped<ILoggedUserRepository, LoggedUserRepository>();
            services.AddScoped<IApplicationLayerRepository, ApplicationLayerRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IApplicationLayerRepository, ApplicationLayerRepository>();
            services.AddScoped<IEnvironmentRepository, EnvironmentRepository>();
            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddControllers();

            services.AddDbContext<CentralErrosContext>();

           

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            services
           .AddMvc()
           .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
           .AddJsonOptions(opt =>
           {
               opt.JsonSerializerOptions.IgnoreNullValues = true;
           });

            ConfigureAuthenticationAuthorization(services, Configuration);
            ConfigureSwagger(services);

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "CentralErros");
            });

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CentralErros",
                    Version = "v1",
                    Description = "Web API do projeto final da Codenation"
                });

                var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT authorization using Bearer"
                    
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            /*/*Scheme = "oauth2",
                            Name = "Bearer",
                            Scheme= "bearer",
                            In = ParameterLocation.Header,*/
                        },
                        new List<string>()
                    }
                });     
            });
        }

        private static void ConfigureAuthenticationAuthorization(IServiceCollection services, IConfiguration Configuration)
        {
            var token = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(Configuration.GetSection(typeof(TokenConfiguration).Name)).Configure(token);
            services.AddSingleton(token);

            var signingConfiguration = new SigningConfiguration();
            services.AddSingleton(signingConfiguration);


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters.IssuerSigningKey = signingConfiguration.Key;
                x.TokenValidationParameters.ValidAudience = token.ValidAudience;
                x.TokenValidationParameters.ValidIssuer = token.ValidIssuer;
                x.TokenValidationParameters.ValidateIssuerSigningKey = token.ValidateIssuerSigningKey;
                x.TokenValidationParameters.ValidateLifetime = token.ValidateLifetime;
                x.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(TokenConfiguration.Policy, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}