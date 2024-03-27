using ItExpertSolution.Server.Models.ItExpertSolution;
using ItExpertSolution.Server.Models.Model.Solutions.Interfaces;
using ItExpertSolution.Server.Models.Model.Solutions.Services;
using Microsoft.EntityFrameworkCore;

namespace ItExpertSolution.Server.Extentions
{
    public static class ServiceExtensions
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ISolutionService, SolutionService>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var template = configuration.GetConnectionString("TEMPLATE");
            var server = configuration["ItExpert_Server"];
            var database = configuration["ItExpert_Database"];
            var user = configuration["ItExpert_User_ID"];
            var password = configuration["ItExpert_Password"];

            var solutionCs = string.Format(template, server, database, user, password);
            services.AddDbContext<ItExpertSolutionDbContext>(opts =>
            {
                opts.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }));
                opts.EnableSensitiveDataLogging();
                opts.EnableDetailedErrors();
               

                opts.UseSqlServer(solutionCs, o =>
                {
                    o.CommandTimeout(4);
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    o.MigrationsHistoryTable("__ef_migrations_history");
                });        
            });           
        }
    }
}
