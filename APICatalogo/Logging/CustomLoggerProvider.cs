using System.Collections.Concurrent;

namespace APICatalogo.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        readonly CustomerLoggerProviderConfiguration loggerConfiguration;
        readonly ConcurrentDictionary<string, CustomerLogger> loggers = new ConcurrentDictionary<string, CustomerLogger>();

        public CustomLoggerProvider(CustomerLoggerProviderConfiguration Config)
        {
            loggerConfiguration = Config;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfiguration));
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}
