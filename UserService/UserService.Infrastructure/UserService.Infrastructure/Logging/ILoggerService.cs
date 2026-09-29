namespace UserService.Infrastructure.Logging
{
    public interface ILoggerService
    {
        void LogInformation(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void LogError(string message, params object[] args);

        void LogError(System.Exception ex, string message, params object[] args);

        void LogCritical(System.Exception ex, string message, params object[] args);
    }
}
