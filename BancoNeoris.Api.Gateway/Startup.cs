using BancoNeoris.Api.Gateway.Handler;
using BancoNeoris.Api.Gateway.Implement;
using BancoNeoris.Api.Gateway.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNeoris.Api.Gateway
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
            services.AddSingleton<ICuentaRemote, CuentaImplement>().AddSingleton<IMovimientoRemote, MovimientoImplement>();

            services.AddHttpClient("GatewayServicio", config => {
                config.BaseAddress = new Uri(Configuration["Services:Gateway"]);
            });            

            services.AddOcelot().AddDelegatingHandler<TransaccionHandler>().AddDelegatingHandler<ReporteHandler>();

            services.AddSwaggerForOcelot(Configuration, (o) =>
            {
                //o.GenerateDocsForAggregates = true;
                //o.GenerateDocsForGatewayItSelf = true;         
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }

            app.UseStaticFiles();
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();
        }
    }
}
