using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using PasswordManager.Application.Interfaces;
using PasswordManager.Infrastructure;
using PasswordManager.Infrastructure.Persistance;
using PasswordManager.Infrastructure.Services;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace PasswordManager
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

            services.AddTransient<UserResolverService>();
            services.AddTransient<ICryptoService,CryptoService>();
            services.AddTransient<IVaultService,VaultService>();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Description = "Enter token",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Scheme = "Bearer"
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
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddDbContext<PasswordManagerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PasswordManagerDB"));
                options.EnableDetailedErrors(true);
                options.EnableSensitiveDataLogging(true);
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
           {
               jwtBearerOptions.RequireHttpsMetadata = false;
               jwtBearerOptions.SaveToken = true;
               jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
           });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                //    .AllowCredentials()
                );
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
        }
        private void LogError(string message)
        {

            string source;
            string log;

            source = "ParSystemsErp";
            log = "Application";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(source, log);

                EventLog.WriteEntry(source, message);
                EventLog.WriteEntry(source, message, EventLogEntryType.Error);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment() && true == false)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            try
                            {
                                LogError(error.Error.ToString());
                                if (error.Error is DbUpdateException)
                                {

                                    context.Response.AddApplicationError(error.Error.InnerException.Message);
                                    await context.Response.WriteAsync(error.Error.InnerException.Message);
                                }
                                else
                                {
                                    context.Response.AddApplicationError(error.Error.Message);
                                    await context.Response.WriteAsync(error.Error.Message);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    });
                });
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");


            app.UseAuthentication();
            app.UseSpaStaticFiles();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My api v1");
            });
            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            */
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action=Index}/{id?}");
                //  routes.MapSpaFallbackRoute(name: "spa -fallback", defaults: new { controller = "Home", action = "Index" });

            });
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = new System.TimeSpan(0, 0, 120);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}
