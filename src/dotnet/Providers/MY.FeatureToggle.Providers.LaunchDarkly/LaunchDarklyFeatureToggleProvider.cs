using LaunchDarkly.Client;
using MY.FeatureToggle.Constants;
using MY.FeatureToggle.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace MY.FeatureToggle.Providers.LaunchDarkly
{
    public abstract class LaunchDarklyFeatureToggleProvider : IFeatureToggleProvider
    {
        private readonly ILdClient _ldClient;
        private User _user;

        protected LaunchDarklyFeatureToggleProvider(ILdClient ldClient)
        {
            _ldClient = ldClient;
        }

        protected abstract User GetUser(IDictionary<string, object> userAttributes = null);

        public bool IsEnable(Enum featureFlag, IDictionary<string, object> userAttributes = null)
        {
            _user = GetUser(userAttributes);
            var feature = featureFlag.GetFlag();

            return _ldClient.BoolVariation(feature.Key, _user, (bool)feature.DefaultValue);
        }

        public bool IsNotEnable(Enum featureFlag, IDictionary<string, object> userAttributes)
        {
            return !IsEnable(featureFlag, userAttributes);
        }

        public T Get<T>(Enum featureFlag, IDictionary<string, object> userAttributes = null)
        {
            var value = default(T);
            var type = typeof(T);
            _user = GetUser(userAttributes);
            var feature = featureFlag.GetFlag();

            switch (feature.Type)
            {
                case VariationType.Boolean:
                    if (feature.Type != VariationType.Boolean && type == typeof(bool))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be bool");

                    value = (T)Convert.ChangeType(_ldClient.BoolVariation(feature.Key, _user, (bool)feature.DefaultValue), typeof(T));
                    break;
                case VariationType.String:
                    if (feature.Type != VariationType.String && type == typeof(string))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be string");

                    value = (T)Convert.ChangeType(_ldClient.StringVariation(feature.Key, _user, (string)feature.DefaultValue), typeof(T));
                    break;
                case VariationType.Number:
                    if (feature.Type != VariationType.Number && type == typeof(int))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be int");

                    value = (T)Convert.ChangeType(_ldClient.IntVariation(feature.Key, _user, (int)feature.DefaultValue), typeof(T));
                    break;
                case VariationType.Float:
                    if (feature.Type != VariationType.Float && type == typeof(float))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be float");

                    value = (T)Convert.ChangeType(_ldClient.FloatVariation(feature.Key, _user, (float)feature.DefaultValue), typeof(T));
                    break;
                case VariationType.ArrayOfString:
                    if (feature.Type != VariationType.ArrayOfString && type == typeof(string))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be array of string");
                    var commaSeperateStr = _ldClient.StringVariation(feature.Key, _user, (string)feature.DefaultValue);

                    value = (T)Convert.ChangeType(commaSeperateStr.Split(new[] { ',' }, StringSplitOptions.None), typeof(T));
                    break;
                case VariationType.Json:
                    if (feature.Type != VariationType.Json && type == typeof(JToken))
                        throw new InvalidCastException($"Feature flag is configure to be {feature.Type} except to be JToken");

                    value = (T)Convert.ChangeType(_ldClient.JsonVariation(feature.Key, _user, (JToken)feature.DefaultValue), typeof(T));
                    break;
            }

            return value;
        }
    }
}
