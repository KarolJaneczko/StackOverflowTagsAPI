using Newtonsoft.Json;
using StackOverflowTagsAPI.Models.API;
using StackOverflowTagsAPI.Services.Interfaces;

namespace StackOverflowTagsAPI.Services {
    public class StorageService : IStorageService {
        public string CachePath { get; set; }

        public StorageService() {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            CachePath = Path.Combine(directory, "cache.txt");
        }

        public Task SaveTags(IEnumerable<TagInfo> tagInfos) {
            string jsonString = JsonConvert.SerializeObject(tagInfos);
            File.WriteAllText(CachePath, jsonString);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TagInfo>> GetTags() {
            var cacheData = File.ReadAllText(CachePath);
            var tagInfos = JsonConvert.DeserializeObject<IEnumerable<TagInfo>>(cacheData);
            return Task.FromResult(tagInfos);
        }
    }
}
