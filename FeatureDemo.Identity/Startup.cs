using System.Linq;
using System.Reflection;
using FeatureDemo.Identity.Context;
using FeatureDemo.Identity.Utilities;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace FeatureDemo.Identity
{
    public class Startup
    {
        public Startup(ILoggerFactory loggerFactory, IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
			AppSettings.Configuration = configuration;
			AppSettings.Environment = env;

			//var serilog = new LoggerConfiguration()
				//.MinimumLevel.Verbose()
				//.Enrich.FromLogContext()
				//.WriteTo.File(@"identityserver4_log.txt")
				//.WriteTo.LiterateConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message}{NewLine}{Exception}{NewLine}");

            //loggerFactory
                //.AddDebug()
                //.AddSerilog(serilog.CreateLogger());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
		{
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //services.AddDbContext<ApplicationDbContext>(opts => opts.UseMySql(AppSettings.AuthDatabase));
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            var signingCreds = new SigningCredentials(AppSettings.RsaSecurityKey, SecurityAlgorithms.RsaSha256Signature);

            services.AddIdentityServer()
                    .AddOperationalStore(builder => builder.UseMySql(AppSettings.AuthDatabase, opts => opts.MigrationsAssembly(migrationsAssembly)))
                    .AddConfigurationStore(builder => builder.UseMySql(AppSettings.AuthDatabase, opts => opts.MigrationsAssembly(migrationsAssembly)))
                    .AddAspNetIdentity<IdentityUser>()
					.AddSigningCredential(signingCreds)
					//.AddInMemoryApiResources(AppSettings.ApiResources)
					//.AddInMemoryClients(AppSettings.ApiClients)
					//.AddTestUsers(AppSettings.TestUsers)
                    ;

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitializeDbTestData(app);

            app.UseAuthentication();
            app.UseIdentityServer();

            //app.UseMvc();
        }

        private static void InitializeDbTestData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context?.Database.Migrate();



                if (!context.Clients.Any())
                {
                    foreach (var client in AppSettings.ApiClients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in AppSettings.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in AppSettings.ApiResources)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                if (!userManager.Users.Any())
                {
                    foreach (var testUser in AppSettings.TestUsers)
                    {
                        var identityUser = new IdentityUser(testUser.Username)
                        {
                            Id = testUser.SubjectId
                        };

                        userManager.AddClaimsAsync(identityUser,testUser.Claims);

                        userManager.CreateAsync(identityUser, "Password123!").Wait();
                    }
                }
            }
        }
    }
}
