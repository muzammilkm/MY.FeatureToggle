using LaunchDarkly.Client;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MY.FeatureToggle.Providers.LaunchDarkly.Extensions
{
    public static class UserExtensions
    {
        public static User FromAttibutes(this User user, IDictionary<string, object> userAttributes = null)
        {
            if (userAttributes == null)
                return user;

            foreach (var userAttribute in userAttributes)
            {
                switch (userAttribute.Key)
                {
                    case "key":
                        user.Key = userAttribute.Value.ToString();
                        break;
                    case "secondary":
                        user.SecondaryKey = userAttribute.Value.ToString();
                        break;
                    case "ip":
                        user.IpAddress = userAttribute.Value.ToString();
                        break;
                    case "email":
                        user.Email = userAttribute.Value.ToString();
                        break;
                    case "avatar":
                        user.Avatar = userAttribute.Value.ToString();
                        break;
                    case "firstName":
                        user.FirstName = userAttribute.Value.ToString();
                        break;
                    case "lastName":
                        user.LastName = userAttribute.Value.ToString();
                        break;
                    case "name":
                        user.Name = userAttribute.Value.ToString();
                        break;
                    case "country":
                        user.Country = userAttribute.Value.ToString();
                        break;
                    case "anonymous":
                        user.Anonymous = bool.Parse(userAttribute.Value.ToString());
                        break;
                    default:
                        user.Custom.Add(userAttribute.Key, new JValue(userAttribute.Value));
                        break;
                }
            }

            return user;
        }
    }
}
