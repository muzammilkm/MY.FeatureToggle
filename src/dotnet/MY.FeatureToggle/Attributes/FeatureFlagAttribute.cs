using MY.FeatureToggle.Constants;
using System;

namespace MY.FeatureToggle.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FeatureFlagAttribute : Attribute
    {
        public FeatureFlagAttribute(string key, bool defaultValue = false)
        {
            Key = key;
            DefaultValue = defaultValue;
            Type = VariationType.Boolean;
        }

        public FeatureFlagAttribute(string key, VariationType type, object defaultValue = null)
        {
            Key = key;
            DefaultValue = defaultValue;
            Type = type;
        }

        public string Key { get; }

        public object DefaultValue { get; }

        public VariationType Type { get; }
    }
}
