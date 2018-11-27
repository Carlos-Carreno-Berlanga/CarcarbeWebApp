using ConsoleDaemonProducer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDaemonProducer.Services.Implementation
{
    public class EnviromentVariableService : IEnviromentVariableService
    {
        ILogger<EnviromentVariableService> _logger;
        public EnviromentVariableService(ILogger<EnviromentVariableService> logger)
        {
            _logger = logger;
        }
        public void CreateIfNotExists(string enviromentVariableName, string enviromentVariableValue)
        {
            try
            {
                 
                string value = Environment.GetEnvironmentVariable(enviromentVariableName, EnvironmentVariableTarget.Machine);
                if (string.IsNullOrEmpty(value))
                {
                    Environment.SetEnvironmentVariable(enviromentVariableName, enviromentVariableValue, EnvironmentVariableTarget.Machine);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
