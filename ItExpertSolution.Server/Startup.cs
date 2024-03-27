using AutoMapper;
using ItExpertSolution.Server.Extentions;
using Microsoft.OpenApi.Models;

namespace ItExpertSolution.Server
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureService();
            services.ConfigureDbContext(_configuration);
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IMapper mapper)
        {
            app.UseSwagger(c => { c.PreSerializeFilters.Add((swaggerDoc, httpReq) => { swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host.Value}" } }; }); });
            app.UseSwaggerUI(c => { c.SwaggerEndpoint($"v1/swagger.json", "ItExpertSolution Server v1"); });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            mapper.ConfigurationProvider.AssertConfigurationIsValid(); //check automapper fields on project build

        }
    }
}
