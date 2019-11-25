using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Execution.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestGrapQL.Extensions;
using TestGrapQL.Schema;

namespace TestGrapQL
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
            Assembly assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(assembly);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnectionString");
                options.UseSqlServer(connectionString);
            });

            services.RegisterServices(ServiceLifetime.Scoped, assembly);

            // Add GraphQL Services
            services.AddGraphQL(sp => SchemaBuilder.New()
                    .AddServices(sp)
                    // Adds the authorize directive and enable the authorization middleware.
                    .AddAuthorizeDirectiveType()
                    .AddQueryType<QueryType>()
                    .AddMutationType<MutationType>()
                    .ScanGraphTypes(assembly)
                    .Create()
                , new QueryExecutionOptions
                {
                    TracingPreference = TracingPreference.Never
                }
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseWebSockets()
                .UseGraphQL("/graphql")
                .UseGraphiQL("/graphql")
                .UsePlayground("/graphql", "/graphql")
                .UseVoyager("/graphql");

            app.UseMvc();

            db.EnsureSeedData();
        }
    }
}
