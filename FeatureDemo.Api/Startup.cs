using FeatureDemo.Api.Context;
using FeatureDemo.Api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using FeatureDemo.Api.Utilities;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FeatureDemo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            AppSettings.Configuration = configuration;
            AppSettings.Environment = env;
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

			services.AddDbContext<FeatureContext>(
				ops => ops.UseMySql(AppSettings.FeatureDatabase, mysqlOps => mysqlOps.MaxBatchSize(AppSettings.MaxBatchSize)), ServiceLifetime.Scoped);
            
            services.AddAuthentication(opts => opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opts => {
                        opts.Authority = "http://localhost:5100";
                        opts.Audience = "fd.client";
                        opts.RequireHttpsMetadata = false;
                    });


            services.AddScoped(typeof(IRepository), typeof(MySqlRepository));
            //services.AddScoped(typeof(IRepository), typeof(MockRepository));
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

				app.Use(async (context, nxt) =>
				{
					foreach (var header in context.Request.Headers)
					{
						Debug.WriteLine($"{header.Key} : {header.Value}");
					}

					await nxt?.Invoke();
				});
            }
            app.UseStaticFiles();
            app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
			});
        }
    }
}
