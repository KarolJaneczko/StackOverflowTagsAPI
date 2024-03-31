using StackOverflowTagsAPI.Models.API;
using StackOverflowTagsAPI.Models.Logic;
using StackOverflowTagsAPI.Models.StackOverflow;
using StackOverflowTagsAPI.Services.Interfaces;
using StackOverflowTagsAPI.Utils;

namespace StackOverflowTagsAPI.Services {
    public class TagService(IHttpService httpService, IStorageService storageService) : ITagService {
        private readonly IHttpService HttpService = httpService;
        private readonly IStorageService StorageService = storageService;

        public async Task<ApiResponse> SetTags(CancellationToken cancellationToken) {
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

        public async Task<ApiResponse> GetTagInfos(TagInfoRequest tagInfoRequest, CancellationToken cancellationToken) {
            async Task<ApiResponse> task() {
                if (tagInfoRequest.ResultsPerPage < 0) {
                    throw new ApiException("Number of results per page cannot be negative!");
                }

                if (tagInfoRequest.Page - 1 < 0) {
                    throw new ApiException("Number of the page must start from 1.");
                }

                IEnumerable<TagInfo> resultTagInfos;
                var tagInfos = await StorageService.GetTags();

                if (tagInfoRequest.SortType == SortType.Alphabetical) {
                    resultTagInfos = tagInfos.OrderBy(x => x.Name);
                } else {
                    resultTagInfos = tagInfos.OrderBy(x => x.PercentageShare);
                }

                if (tagInfoRequest.DescendingOrder) {
                    resultTagInfos = resultTagInfos.Reverse();
                }

                resultTagInfos = resultTagInfos.Skip((tagInfoRequest.Page - 1) * tagInfoRequest.ResultsPerPage).Take(tagInfoRequest.ResultsPerPage);

                var tagInfoResponse = new TagInfoResponse {
                    Page = tagInfoRequest.Page,
                    ResultsPerPage = tagInfoRequest.ResultsPerPage,
                    TagInfos = resultTagInfos,
                };

                return new ApiResponse(true, tagInfoResponse);
            }
            return await ClassHelper.RunWithTryCatch(task);
        }
    }
}
