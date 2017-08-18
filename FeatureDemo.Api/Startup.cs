using FeatureDemo.Api.Context;
using FeatureDemo.Api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Swagger;
using FeatureDemo.Api.Utilities;
using System.Diagnostics;
using System.Collections.Generic;

namespace FeatureDemo.Api
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
            services.AddMvc();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "API V1", Version = "v1" });
			});

            ConfigureEntityFramework(services);

            services.AddSingleton(typeof(IRepository), typeof(MockRepository));
            services.AddTransient(typeof(IDbContext), typeof(FeatureContext));
        }

        public void ConfigureEntityFramework(IServiceCollection services)
        {
            Debug.WriteLine($"Using ConnectionString {AppSettings.FeatureDatabase}");
            Debug.WriteLine($"Using MaxBatchSize of {AppSettings.MaxBatchSize}");

            services.AddDbContext<FeatureContext>(
                ops => ops.UseMySql(AppSettings.FeatureDatabase, mysqlOps => mysqlOps.MaxBatchSize(AppSettings.MaxBatchSize)), ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
			});
        }
    }
}
