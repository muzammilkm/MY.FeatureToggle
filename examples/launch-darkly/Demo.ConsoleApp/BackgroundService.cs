using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MY.FeatureToggle;
using MY.FeatureToggle.Attributes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.ConsoleApp
{
    public enum Features
    {
        [FeatureFlag("beta-customer")]
        BetaCustomer
    }

    public class BackgroundService : IHostedService, IDisposable
    {
        #region Private
        private readonly ILogger<BackgroundService> _logger;
        private readonly IFeatureToggleProvider _featureToggleProvider;
        private Timer _timer;
        #endregion

        public BackgroundService(ILogger<BackgroundService> logger, IFeatureToggleProvider featureToggleProvider)
        {
            _logger = logger;
            _featureToggleProvider = featureToggleProvider;
        }

        private void DoWork(object state)
        {
            var userAttributes = new Dictionary<string, object>
            {
                { "userName", "muzammil.khaja" }
            };            

            if (_featureToggleProvider.IsEnable(Features.BetaCustomer, userAttributes))                
            {
                // Another way to access feature flag
                // Features.BetaCustomer.IsEnable(_featureToggleProvider, userAttributes))
                _logger.LogInformation($"Beta Customer is Enabled");
            }
            else
            {
                _logger.LogWarning("Beta Customer is Disabled");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
