using Microsoft.Extensions.Logging;
using Rebus.Logging;
using System;

namespace Carcarbe.Shared.Logging
{
    public class MSLoggerFactoryAdapter : AbstractRebusLoggerFactory
    {
        private readonly ILoggerFactory _logger;

        public MSLoggerFactoryAdapter(ILoggerFactory logger)
        {
            _logger = logger;
        }

        protected override ILog GetLogger(Type type)
        {
            return new MSLoggerAdapter(_logger.CreateLogger(type));
        }
    }
}
