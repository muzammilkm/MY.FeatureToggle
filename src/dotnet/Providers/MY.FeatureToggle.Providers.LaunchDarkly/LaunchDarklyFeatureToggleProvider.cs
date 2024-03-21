using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server;
using MY.FeatureToggle.Constants;
using MY.FeatureToggle.Extensions;
using System;
using System.Collections.Generic;

namespace MY.FeatureToggle.Providers.LaunchDarkly
{
    public abstract class LaunchDarklyFeatureToggleProvider : IFeatureToggleProvider
    {
        private readonly LdClient _ldClient;
        private Context _context;

        protected LaunchDarklyFeatureToggleProvider(LdClient ldClient)
        {
            _ldClient = ldClient;
        }

        protected abstract Context GetContext(IDictionary<string, object> contextAttributes = null);

        public bool IsEnable(Enum featureFlag, IDictionary<string, object> userAttributes = null)
        {
            _context = GetContext(userAttributes);
            var feature = featureFlag.GetFlag();

            return _ldClient.BoolVariation(feature.Key, _context, (bool)feature.DefaultValue);
        }

        public bool IsNotEnable(Enum featureFlag, IDictionary<string, object> userAttributes)
        {
            return !IsEnable(featureFlag, userAttributes);
        }

        public T Get<T>(Enum featureFlag, IDictionary<string, object> userAttributes = null)
        {
            var value = default(T);
            var type = typeof(T);
            _context = GetContext(userAttributes);
            var feature = featureFlag.GetFlag();

            switch (feature.Type)
            {
                case VariationType.Boolean:
                    if (feature.Type != VariationType.Boolean && type == typeof(bool))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be bool");

                    value = (T)Convert.ChangeType(_ldClient.BoolVariation(feature.Key, _context, (bool)feature.DefaultValue), typeof(T));
                    break;
                case VariationType.String:
                    if (feature.Type != VariationType.String && type == typeof(string))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be string");

                    value = (T)Convert.ChangeType(_ldClient.StringVariation(feature.Key, _context, (string)feature.DefaultValue), typeof(T));
                    break;
                case VariationType.Number:
                    if (feature.Type != VariationType.Number && type == typeof(int))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be int");

                    value = (T)Convert.ChangeType(_ldClient.IntVariation(feature.Key, _context, (int)feature.DefaultValue), typeof(T));
                    break;
                case VariationType.Float:
                    if (feature.Type != VariationType.Float && type == typeof(float))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be float");

                    value = (T)Convert.ChangeType(_ldClient.FloatVariation(feature.Key, _context, (float)feature.DefaultValue), typeof(T));
                    break;
                case VariationType.ArrayOfString:
                    if (feature.Type != VariationType.ArrayOfString && type == typeof(string))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be array of string");
                    var commaSeperateStr = _ldClient.StringVariation(feature.Key, _context, (string)feature.DefaultValue);

                    value = (T)Convert.ChangeType(commaSeperateStr.Split(new[] { ',' }, StringSplitOptions.None), typeof(T));
                    break;
            }

            return value;
        }
    }
}
