# MY.FeatureToggle

Easy & better management of your feature flags with easy to setup and dependency injection.

## Providers

## LaunchDarkly

  ![MY.FeatureToggle](https://github.com/muzammilkm/MY.FeatureToggle/workflows/MY.FeatureToggle/badge.svg)

  > And also support launch darkly as a provider along with middleware.

Please follow the steps

* [Creating feature flags](https://docs.launchdarkly.com/docs/creating-a-feature-flag) in launch darkly

* Consume Feature flag in your project
  * Add Reference MY.FeatureToggle package

  ``` json
    PM > Install-Package MY.FeatureToggle.Providers.LaunchDarkly
  ```

  * Set SDK in appsetting.json

  ``` json
    "LaunchDarkly": {
        "SdkKey": "sdk-key"
    }
  ```

  * Configure & inject Launch Darkly

  ``` c#
     services
        .AddLaunchDarkly(Configuration)
        .AddTransient<IFeatureToggleProvider, AnonymousFeatureToggleProvider>();
  ```

  * Create enums & Decorate each enum with FeatureFlag which takes launch darkly key & default value

  ``` csharp
    public enum Features
    {
      [FeatureFlag("sample-feature-1", false)]
      SampleFeatureOne
    }
  ```

  * Inject IFeatureToggleProvider 
  * Consume Toggles

  ``` csharp
    var userAttributes = new Dictionary<string, object>
    {
        { "userName", "muzammil.km" }
    };

    var isFeatureEnabled = _featureToggle.IsEnable(Features.SampleFeatureOne, userAttributes);
   ```
