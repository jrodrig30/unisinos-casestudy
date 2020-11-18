using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Unisinos.CaseStudy.API.Jobs
{
    // Este HostedService pode futuramente abrigar implementações de long-running jobs
    public class BemPromotoraBackgroundJob : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
