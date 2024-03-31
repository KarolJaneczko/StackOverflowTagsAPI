using Microsoft.AspNetCore.Mvc;
using StackOverflowTagsAPI.Models.Logic;

namespace StackOverflowTagsAPI.Controllers {
    public class ApiControllerBase<T>(ILogger<T> logger) : ControllerBase {
        private ILogger<T> Logger { get; } = logger;

        protected async Task<ApiResponse> RunWithTryCatch(Func<Task<ApiResponse>> method) {
            try {
                var response = await method();
                if (!response.IsSuccess) {
                    throw response.GetException();
                } else {
                    return response;
                }
            } catch (ApiException ex) {
                Logger.LogError(ex, "{Message}", ex.ResponseMessage);
                return new ApiResponse(ex);
            } catch (Exception ex) {
                Logger.LogError(ex, "{Message}", ex.Message);
                return new ApiResponse(ex);
            }
        }
    }
}
