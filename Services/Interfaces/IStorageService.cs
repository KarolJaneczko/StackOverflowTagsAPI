using StackOverflowTagsAPI.Models.API;

namespace StackOverflowTagsAPI.Services.Interfaces {
    public interface IStorageService {
        public Task SaveTags(IEnumerable<TagInfo> tagInfos);
    }
}
