using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server;
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

        protected override Context GetContext(IDictionary<string, object> userAttributes = null)
        {
            return Context
                .Builder(_key)
                .Anonymous(true)
                .FromAttibutes(userAttributes);
        }
    }
}
