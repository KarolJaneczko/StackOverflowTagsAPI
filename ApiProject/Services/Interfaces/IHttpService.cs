using StackOverflowTagsAPI.Models.Logic;

namespace StackOverflowTagsAPI.Services.Interfaces {
    public interface IHttpService {
        public Task<ApiResponse> SendApiRequest<T>(HttpMethod httpMethod, string path, object body = null, CancellationToken cancellationToken = default);
    }
}
