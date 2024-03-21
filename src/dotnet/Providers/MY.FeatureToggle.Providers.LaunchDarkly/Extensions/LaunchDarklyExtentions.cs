using LaunchDarkly.Sdk.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MY.FeatureToggle.Providers.LaunchDarkly.Options;
using System;

namespace MY.FeatureToggle.Providers.LaunchDarkly.Extensions
{
    public static class LaunchDarklyExtentions
    {
        public static IServiceCollection AddLaunchDarkly(this IServiceCollection services, IConfiguration configuration,
            Action<LaunchDarlyOptions> setupAction = null)
        {
            var launchDarklyKey = configuration["LaunchDarkly:SdkKey"];
            var launchDarklyConfig = Configuration.Default(launchDarklyKey);

            setupAction?.Invoke(new LaunchDarlyOptions { Configuration = launchDarklyConfig });

            var launchDarklyClient = new LdClient(launchDarklyConfig);

            services
                .AddSingleton<LdClient>(launchDarklyClient);

            return services;
        }
    }
}
