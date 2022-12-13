using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server;
using LaunchDarkly.Sdk.Server.Interfaces;
using MY.FeatureToggle.Providers.LaunchDarkly.Extensions;
using System;
using System.Collections.Generic;

namespace MY.FeatureToggle.Providers.LaunchDarkly
{
    public class AnonymousFeatureToggleProvider : LaunchDarklyFeatureToggleProvider
    {
        private readonly string _key;

        public AnonymousFeatureToggleProvider(LdClient ldClient)
            : base(ldClient)
        {
            _key = Guid.NewGuid().ToString();
        }

        protected override User GetUser(IDictionary<string, object> userAttributes = null)
        {
            return User.Builder(_key)
                .Anonymous(true)
                .FromAttibutes(userAttributes);
        }
    }
}
