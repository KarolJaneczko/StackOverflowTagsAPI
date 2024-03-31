using StackOverflowTagsAPI.Models.API;
using StackOverflowTagsAPI.Models.Logic;
using StackOverflowTagsAPI.Models.StackOverflow;
using StackOverflowTagsAPI.Services.Interfaces;
using StackOverflowTagsAPI.Utils;

namespace StackOverflowTagsAPI.Services {
    public class TagService(IHttpService httpService, IStorageService storageService) : ITagService {
        private readonly IHttpService HttpService = httpService;
        private readonly IStorageService StorageService = storageService;

        public async Task<ApiResponse> GetTags(CancellationToken cancellationToken) {
            async Task<ApiResponse> task() {
                var tagsResponses = new List<TagsResponse>();

                // Pobieramy 10 stron, zawierających po 100 sztuk tagów. API StackOverflow nie umożliwia nam pobrania więcej, niż 100 sztuk per odpowiedź.
                for (var i = 1; i <= 10; i++) {
                    var tagsResponse = await HttpService.SendApiRequest<TagsResponse>(HttpMethod.Get, $"tags?page={i}&pagesize=100&order=desc&sort=popular&site=stackoverflow", cancellationToken: cancellationToken);
                    tagsResponses.Add(tagsResponse.Data as TagsResponse);
                }

                var tags = tagsResponses.SelectMany(x => x.Items).ToList();
                var tagInfos = tags.ConvertAll(x => new TagInfo(x));
                var count = tagInfos.Sum(x => x.Count);
                tagInfos.ForEach(x => x.PercentageShare = decimal.Round(x.Count / (decimal)count, 4));

                await StorageService.SaveTags(tagInfos);
                return new ApiResponse(true, "Success", "Tokens has been saved and overwritten in the storage.");
            }
            return await ClassHelper.RunWithTryCatch(task);
        }
    }
}
