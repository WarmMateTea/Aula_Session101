using Session101;
using Microsoft.AspNetCore.Hosting;

namespace Session101
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            //evita a exception de banco de dados inexistente
            .UseDefaultServiceProvider(options => options.ValidateScopes = false);
    }
}