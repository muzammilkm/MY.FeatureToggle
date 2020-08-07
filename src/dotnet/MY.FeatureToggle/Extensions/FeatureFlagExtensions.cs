using MY.FeatureToggle.Attributes;
using System;
using System.Linq;


namespace MY.FeatureToggle.Extensions
{
    public static class FeatureFlagExtensions
    {
        public static FeatureFlagAttribute GetFlag(this Enum featureFlag)
        {
            return featureFlag
                .GetType()
                .GetMember(featureFlag.ToString())
                .First()
                .GetCustomAttributes(typeof(FeatureFlagAttribute), false)
                .OfType<FeatureFlagAttribute>()
                .LastOrDefault();
        }
    }
}
