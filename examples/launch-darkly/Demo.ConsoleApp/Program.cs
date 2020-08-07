using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using MY.FeatureToggle.Providers.LaunchDarkly.Extensions;
using MY.FeatureToggle.Providers.LaunchDarkly;
using MY.FeatureToggle;
using Microsoft.Extensions.Logging;

namespace Demo.ConsoleApp
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder
                        .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: false);
                })
                .ConfigureLogging((hostContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, service) =>
                {
                    service
                        .AddHostedService<BackgroundService>()
                        .AddLaunchDarkly(hostContext.Configuration)
                        .AddTransient<IFeatureToggleProvider, AnonymousFeatureToggleProvider>();
                });
    }
}
