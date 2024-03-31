using StackOverflowTagsAPI.Models.Logic;

namespace StackOverflowTagsAPI.Services.Interfaces {
    public interface ITagService {
        public Task<ApiResponse> GetTags(CancellationToken cancellationToken);
    }
}
