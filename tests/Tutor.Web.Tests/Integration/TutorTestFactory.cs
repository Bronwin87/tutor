﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using Tutor.Infrastructure.Database;
using Tutor.Infrastructure.Database.EventStore;
using Tutor.Infrastructure.Security;

namespace Tutor.Web.Tests.Integration
{
    public class TutorApplicationTestFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TutorContext>));
                services.Remove(descriptor);
                var eventDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EventContext>));
                services.Remove(eventDescriptor);

                var connectionString = CreateConnectionStringForTest();
                services.AddDbContext<TutorContext>(opt =>
                    opt.UseNpgsql(connectionString));
                services.AddDbContext<EventContext>(opt =>
                    opt.UseNpgsql(connectionString));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TutorContext>();
                var eventDb = scopedServices.GetRequiredService<EventContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<TutorApplicationTestFactory<TStartup>>>();

                db.Database.EnsureCreated();
                InitializeEventDbForTests(eventDb);

                try
                {
                    InitializeDbForTests(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                                        "database with test messages. Error: {Message}", ex.Message);
                }
            });
        }

        private static void InitializeDbForTests(TutorContext db)
        {
            var testScripts = Directory.GetFiles("../../../TestData/Scripts/");
            var startingDb = string.Join('\n', testScripts.Select(File.ReadAllText));
            db.Database.ExecuteSqlRaw(startingDb);
        }

        private static void InitializeEventDbForTests(EventContext db)
        {
            var createScript = db.Database.GenerateCreateScript();
            try
            {
                db.Database.ExecuteSqlRaw(createScript);
            }
            catch (Exception) { }
        }

        private static string CreateConnectionStringForTest()
        {
            var server = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432";
            var database = EnvironmentConnection.GetSecret("DATABASE_SCHEMA") ?? "smart-tutor-test";
            var user = EnvironmentConnection.GetSecret("DATABASE_USERNAME") ?? "postgres";
            var password = EnvironmentConnection.GetSecret("DATABASE_PASSWORD") ?? "super";
            var integratedSecurity = Environment.GetEnvironmentVariable("DATABASE_INTEGRATED_SECURITY") ?? "false";
            var pooling = Environment.GetEnvironmentVariable("DATABASE_POOLING") ?? "true";

            return
                $"Server={server};Port={port};Database={database};User ID={user};Password={password};Integrated Security={integratedSecurity};Pooling={pooling};";
        }
    }
}
