using StackOverflowTagsAPI.Models.API;
using StackOverflowTagsAPI.Models.Logic;

namespace StackOverflowTagsAPI.Services.Interfaces {
    public interface ITagService {
        public Task<ApiResponse> SetTags(CancellationToken cancellationToken);
        public Task<ApiResponse> GetTagInfos(TagInfoRequest tagInfoRequest, CancellationToken cancellationToken);
    }
}
