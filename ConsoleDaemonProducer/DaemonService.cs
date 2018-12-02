using Carcarbe.Shared.Messages;
using Carcarbe.Shared.Repository;
using ConsoleDaemonProducer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rebus.Bus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDaemonProducer
{
    public class DaemonService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IOptions<DaemonConfig> _config;
        private readonly IBus _bus;
        private IEnviromentVariableService _enviromentVariableService;
        private IMeasurementService _measurementService;
        private readonly IServiceProvider _services;
        private IMeasurementRepository _measurementRepository;
        public DaemonService(ILogger<DaemonService> logger,
            IOptions<DaemonConfig> config,
            IBus bus,
            IServiceProvider services
            )
        {
            _logger = logger;
            _config = config;
            _bus = bus;
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting daemon: " + _config.Value.DaemonName);

            using (var scope = _services.CreateScope())
            {
                _enviromentVariableService = scope.ServiceProvider.GetRequiredService<IEnviromentVariableService>();
                _measurementService = scope.ServiceProvider.GetRequiredService<IMeasurementService>();
                _enviromentVariableService.CreateIfNotExists("COMPUTER_ID", Guid.NewGuid().ToString());
                _measurementRepository = scope.ServiceProvider.GetRequiredService<IMeasurementRepository>();
                while (true)
                {
                    _logger.LogInformation($"{DateTime.Now}: Timed Background Service is running:");

                    await DoWorkAsync();
                    await Task.Delay(_config.Value.TickInterval);
                }
            }

        }

        public async Task DoWorkAsync()
        {
            var senseHatmeasurement = _measurementService.Measure();

            await _measurementService.SaveAsync(senseHatmeasurement);
            var measurements = _measurementService.SplitSenseHatMeasurement(senseHatmeasurement);
            await _bus.Send(new MeterMessage(JsonConvert.SerializeObject(measurements)));
            _logger.LogInformation("===================================================");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping daemon.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing....");

        }
    }
}
