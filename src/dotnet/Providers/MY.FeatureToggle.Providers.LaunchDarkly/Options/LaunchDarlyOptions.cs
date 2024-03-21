using LaunchDarkly.Sdk.Server;

namespace MY.FeatureToggle.Providers.LaunchDarkly.Options
{
    public class LaunchDarlyOptions
    {
        public Configuration Configuration { get; set; }

        public ConfigurationBuilder ConfigurationBuilder { get; set; }
    }
}
