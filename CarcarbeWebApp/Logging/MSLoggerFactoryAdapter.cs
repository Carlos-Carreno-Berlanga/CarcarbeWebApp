using Microsoft.Extensions.Logging;
using Rebus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarcarbeWebApp.Logging
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
