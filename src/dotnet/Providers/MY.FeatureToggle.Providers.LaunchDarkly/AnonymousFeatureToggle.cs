using LaunchDarkly.Client;
using MY.FeatureToggle.Providers.LaunchDarkly.Extensions;
using System;
using System.Collections.Generic;

namespace MY.FeatureToggle.Providers.LaunchDarkly
{
    public class AnonymousFeatureToggleProvider : BaseFeatureToggleProvider
    {
        private readonly string _key;

        public AnonymousFeatureToggleProvider(ILdClient ldClient)
        : base(ldClient)
        {
            _key = Guid.NewGuid().ToString();
        }

        protected override User GetUser(IDictionary<string, object> userAttributes = null)
        {
            return User.WithKey(_key)
                .AndAnonymous(true)
                .FromAttibutes(userAttributes);
        }
    }
}
