using StackOverflowTagsAPI.Models.Logic;

namespace StackOverflowTagsAPI.Services {
    public class ApiLogger(ApiLoggerConfiguration apiLoggerConfiguration) : ILogger {
        private ApiLoggerConfiguration ApiLoggerConfiguration { get; } = apiLoggerConfiguration;
        private bool IsLoggerEnabled => ApiLoggerConfiguration.IsLoggerEnabled;
        private IEnumerable<string> EnabledLogs => ApiLoggerConfiguration.EnabledLogs;

        public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel) => EnabledLogs.Any(x => x == logLevel.ToString());

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
            if (!IsEnabled(logLevel)) {
                return;
            }

            if (!IsLoggerEnabled) {
                return;
            }

            var logsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
            var log = $"Date: {DateTime.Now} | Level: {logLevel} | {formatter(state, exception)}";
            File.AppendAllLines(logsPath, [log]);
        }
    }
}
