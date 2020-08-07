using System;
using System.Collections.Generic;

namespace MY.FeatureToggle
{
    public interface IFeatureToggleProvider
    {
        bool IsEnable(Enum featureFlag, IDictionary<string, object> userAttributes = null);

        bool IsNotEnable(Enum featureFlag, IDictionary<string, object> userAttributes = null);

        T Get<T>(Enum featureFlag, IDictionary<string, object> userAttributes = null);
    }
}
