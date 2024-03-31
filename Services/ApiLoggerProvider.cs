using Microsoft.Extensions.Options;
using StackOverflowTagsAPI.Models.Logic;

namespace StackOverflowTagsAPI.Services {
    public class ApiLoggerProvider(ApiLoggerConfiguration apiLoggerConfiguration) : ILoggerProvider {
        private readonly ApiLoggerConfiguration ApiLoggerConfiguration = apiLoggerConfiguration;

        public ILogger CreateLogger(string categoryName) {
            return new ApiLogger(ApiLoggerConfiguration);
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}
