using System.Collections.Concurrent;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.Logging.Papertrail
{
    public class PapertrailLoggerProvider : ILoggerProvider
    {
        private readonly PapertrailOptions _currentConfig;
        private readonly ConcurrentDictionary<string, PapertrailLogger> _loggers = new();

        public PapertrailLoggerProvider(IOptionsMonitor<PapertrailOptions> config) => _currentConfig = config.CurrentValue;

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new PapertrailLogger(name, _currentConfig));

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}