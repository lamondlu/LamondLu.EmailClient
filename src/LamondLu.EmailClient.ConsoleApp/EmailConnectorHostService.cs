using LamondLu.EmailClient.Domain.Interface;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.ConsoleApp
{
    public class EmailConnectorHostService : IHostedService
    {
        private ILogger _logger = null;

        public EmailConnectorHostService()
        {
            _logger = (ILogger)EnvironmentConst.Services.GetService(typeof(ILogger));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.Write("Email Service started.");


            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.Write("Email Service stopped.");
            });
        }
    }
}
