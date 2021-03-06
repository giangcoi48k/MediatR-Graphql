﻿using HotChocolate;
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
using MediatrGrapQL.Extensions;
using MediatrGrapQL.Schema;
using HotChocolate.Types;
using EFSecondLevelCache.Core;
using CacheManager.Core;
using System;
using Newtonsoft.Json;

namespace MediatrGrapQL
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

            services.AddEFSecondLevelCache();

            //// Add an in-memory cache service provider
            //services.AddSingleton(typeof(ICacheManager<>), typeof(BaseCacheManager<>));
            //services.AddSingleton(typeof(ICacheManagerConfiguration),
            //    new CacheManager.Core.ConfigurationBuilder()
            //            .WithJsonSerializer()
            //            .WithMicrosoftMemoryCacheHandle(instanceName: "MemoryCache1")
            //            .WithExpiration(ExpirationMode.Absolute, TimeSpan.FromMinutes(10))
            //            .Build());
            // Add Redis cache service provider
            var jss = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            const string redisConfigurationKey = "redis";
            services.AddSingleton(typeof(ICacheManagerConfiguration),
                new CacheManager.Core.ConfigurationBuilder()
                    .WithJsonSerializer(serializationSettings: jss, deserializationSettings: jss)
                    .WithUpdateMode(CacheUpdateMode.Up)
                    .WithRedisConfiguration(redisConfigurationKey, config =>
                    {
                        config.WithAllowAdmin()
                            .WithDatabase(0)
                            .WithEndpoint("localhost", 6379);
                    })
                    .WithMaxRetries(100)
                    .WithRetryTimeout(50)
                    .WithRedisCacheHandle(redisConfigurationKey)
                    .WithExpiration(ExpirationMode.Absolute, TimeSpan.FromMinutes(10))
                    .Build());

            services.AddSingleton(typeof(ICacheManager<>), typeof(BaseCacheManager<>));

            services.RegisterServices(ServiceLifetime.Scoped, assembly);
            services.AddDataLoaderRegistry();

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
                //.UseWebSockets()
                .UseGraphQL("/graphql")
                .UseGraphiQL("/graphql")
                .UsePlayground("/graphql", "/graphql")
                .UseVoyager("/graphql");

            app.UseMvc();

            db.EnsureSeedData();
        }
    }
}
