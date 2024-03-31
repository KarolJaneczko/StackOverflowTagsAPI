using StackOverflowTagsAPI.Models.Logic;

namespace StackOverflowTagsAPI.Utils {
    public static class ClassHelper {
        public static async Task<ApiResponse> RunWithTryCatch(Func<Task<ApiResponse>> method) {
            try {
                return await method();
            } catch (ApiException ex) {
                return new ApiResponse(ex);
            } catch (Exception ex) {
                return new ApiResponse(ex);
            }
        }
    }
}
