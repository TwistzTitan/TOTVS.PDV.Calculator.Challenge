using System;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TOTVS.PDV.Calculator.Challenge.Data;
using TOTVS.PDV.Calculator.Challenge.Model;
using TOTVS.PDV.Calculator.Challenge.Services;
using Microsoft.OpenApi.Models;

namespace TOTVS.PDV.Calculator.Challenge
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
            
            services.AddTransient<IPDVCalculadora,PDVCalculadoraService>();

            services.AddTransient<IRepository<Operacao>, RepositorioOperacao>();

            services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo() { Title = "PDV Calculadora" , Version = "1" , Description = "Calculadora PDV para Obter Troco"}));
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "PDV Calculadora V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
