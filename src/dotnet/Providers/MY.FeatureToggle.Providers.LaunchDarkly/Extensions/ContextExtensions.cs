using LaunchDarkly.Sdk;
using System.Collections.Generic;

namespace MY.FeatureToggle.Providers.LaunchDarkly.Extensions
{
    public static class ContextExtensions
    {
        public static Context FromAttibutes(this ContextBuilder contextBuilder, IDictionary<string, object> ucontextAttributes = null)
        {
            if (ucontextAttributes == null)
                return contextBuilder.Build();

            foreach (var userAttribute in ucontextAttributes)
            {
                switch (userAttribute.Key)
                {
                    case "key":
                        contextBuilder.Key(userAttribute.Value.ToString());
                        break;
                    case "anonymous":
                        contextBuilder.Anonymous(bool.Parse(userAttribute.Value.ToString()));
                        break;
                    default:
                        contextBuilder.Set(userAttribute.Key, LdValue.Of(userAttribute.Value.ToString()));
                        break;
                }
            }

            return contextBuilder.Build();
        }
    }
}
