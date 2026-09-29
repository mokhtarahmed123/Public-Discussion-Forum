using Microsoft.Extensions.Logging;

namespace UserService.Infrastructure.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }
        public void LogInformation(string message, params object[] args)
       => _logger.LogInformation(message, args);

        public void LogWarning(string message, params object[] args)
            => _logger.LogWarning(message, args);

        public void LogError(string message, params object[] args)
            => _logger.LogError(message, args);

        public void LogError(System.Exception ex, string message, params object[] args)
            => _logger.LogError(ex, message, args);

        public void LogCritical(System.Exception ex, string message, params object[] args)
            => _logger.LogCritical(ex, message, args);
    }
}
