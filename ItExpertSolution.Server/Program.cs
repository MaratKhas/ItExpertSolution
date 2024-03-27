using ItExpertSolution.Server;
using System.Reflection;

public static class Program {

    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>();

                       webBuilder.ConfigureAppConfiguration((ctx, cb) =>
                       {
                           IHostEnvironment env = ctx.HostingEnvironment;

                           var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                           cb.AddUserSecrets(appAssembly, optional: true);
                       });
                   });
}
