using System;
using System.Collections.Generic;

namespace MY.FeatureToggle.Extensions
{
    public static class FeatureToggleProviderExtenstions
    {
        public static bool IsEnable(this Enum featureFlag, IFeatureToggleProvider featureToggleProvider,
            IDictionary<string, object> userAttributes = null)
        {
            return featureToggleProvider.IsEnable(featureFlag, userAttributes);
        }

        public static bool IsNotEnable(this Enum featureFlag, IFeatureToggleProvider featureToggleProvider,
            IDictionary<string, object> userAttributes = null)
        {
            return featureToggleProvider.IsNotEnable(featureFlag, userAttributes);
        }

        public static T Get<T>(this Enum featureFlag, IFeatureToggleProvider featureToggleProvider,
            IDictionary<string, object> userAttributes = null)
        {
            return featureToggleProvider.Get<T>(featureFlag, userAttributes);
        }
    }
}
