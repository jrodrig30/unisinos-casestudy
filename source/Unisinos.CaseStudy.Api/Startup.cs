using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Unisinos.CaseStudy.API.Jobs;
using Unisinos.CaseStudy.API.Startup;

namespace Unisinos.CaseStudy
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
            services.AddMvc(config =>
                {
                    config.ReturnHttpNotAcceptable = true;
                    config.RespectBrowserAcceptHeader = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwagger();
            services.AddServices();
            services.AddCors();

            //services.AddAuthentication(authOptions =>
            //{
            //    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(bearerOptions =>
            //{
            //    var paramsValidation = bearerOptions.TokenValidationParameters;
            //    paramsValidation.IssuerSigningKey = "ExemploIssuer";
            //    paramsValidation.ValidAudience = tokenConfigurations.Audience;
            //    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

            //    // Valida a assinatura de um token recebido
            //    paramsValidation.ValidateIssuerSigningKey = true;

            //    // Verifica se um token recebido ainda é válido
            //    paramsValidation.ValidateLifetime = true;

            //    // Tempo de tolerância para a expiração de um token (utilizado
            //    // caso haja problemas de sincronismo de horário entre diferentes
            //    // computadores envolvidos no processo de comunicação)
            //    paramsValidation.ClockSkew = TimeSpan.Zero;
            //});

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto

            // Utilizando InMemory database para evitar muito esforço em configurar um banco local
            services.AddDbContext<Data.BemPromotoraContext>
                (options => options
                  .UseInMemoryDatabase("bem-promotora"));

            //services.AddSignalR();

            services.AddHostedService<BemPromotoraBackgroundJob>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bem Promotora API v2019");
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();

            //app.UseSignalR(route =>
            //{
            //    route.MapHub<PropostasHub>("/propostas");
            //});
        }
    }
}
