using LaunchDarkly.Sdk;
using System.Collections.Generic;

namespace MY.FeatureToggle.Providers.LaunchDarkly.Extensions
{
    public static class UserExtensions
    {
        public static User FromAttibutes(this IUserBuilder userBuilder, IDictionary<string, object> userAttributes = null)
        {
            if (userAttributes == null)
                return userBuilder.Build();

            foreach (var userAttribute in userAttributes)
            {
                switch (userAttribute.Key)
                {
                    case "key":
                        userBuilder.Key(userAttribute.Value.ToString());
                        break;
                    case "ip":
                        userBuilder.IPAddress(userAttribute.Value.ToString());
                        break;
                    case "email":
                        userBuilder.Email(userAttribute.Value.ToString());
                        break;
                    case "avatar":
                        userBuilder.Avatar(userAttribute.Value.ToString());
                        break;
                    case "firstName":
                        userBuilder.FirstName(userAttribute.Value.ToString());
                        break;
                    case "lastName":
                        userBuilder.LastName(userAttribute.Value.ToString());
                        break;
                    case "name":
                        userBuilder.Name(userAttribute.Value.ToString());
                        break;
                    case "country":
                        userBuilder.Country(userAttribute.Value.ToString());
                        break;
                    case "anonymous":
                        userBuilder.Anonymous(bool.Parse(userAttribute.Value.ToString()));
                        break;
                    default:
                        userBuilder.Custom(userAttribute.Key, LdValue.Of(userAttribute.Value.ToString()));
                        break;
                }
            }

            return userBuilder.Build();
        }
    }
}
