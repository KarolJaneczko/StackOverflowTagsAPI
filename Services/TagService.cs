using StackOverflowTagsAPI.Models.API;
using StackOverflowTagsAPI.Models.Logic;
using StackOverflowTagsAPI.Models.StackOverflow;
using StackOverflowTagsAPI.Services.Interfaces;

namespace StackOverflowTagsAPI.Services {
    public class TagService(IHttpService httpService, IStorageService storageService) : ITagService {
        private readonly IHttpService HttpService = httpService;
        private readonly IStorageService StorageService = storageService;
        private const string TagsRoute = "tags?page={i}&pagesize=100&order=desc&sort=popular&site=stackoverflow";

        public async Task<ApiResponse> SetTags(CancellationToken cancellationToken) {
            var tagsResponses = new List<TagsResponse>();

            // Pobieramy 10 stron, zawierających po 100 sztuk tagów. API StackOverflow nie umożliwia nam pobrania więcej, niż 100 sztuk per odpowiedź.
            for (var i = 1; i <= 10; i++) {
                var apiResponse = await HttpService.SendApiRequest<TagsResponse>(HttpMethod.Get, TagsRoute.Replace("{i}", i.ToString()), cancellationToken: cancellationToken);
                if (apiResponse.IsSuccess) {
                    tagsResponses.Add(apiResponse.Data as TagsResponse);
                } else {
                    throw new ApiException(apiResponse.Title, apiResponse.Message);
                }
            }

            var tags = tagsResponses.SelectMany(x => x.Items).ToList();
            var tagInfos = tags.ConvertAll(x => new TagInfo(x));
            var count = tagInfos.Sum(x => x.Count);
            tagInfos.ForEach(x => x.PercentageShare = decimal.Round(x.Count / (decimal)count, 4));

            await StorageService.SaveTags(tagInfos);
            return new ApiResponse(true, "Success", "Tokens has been saved and overwritten in the storage.");
        }

        public async Task<ApiResponse> GetTagInfos(TagInfoRequest tagInfoRequest, CancellationToken cancellationToken) {
            if (tagInfoRequest.ResultsPerPage < 0) {
                throw new ApiException("Number of results per page cannot be negative!");
            }
            if (tagInfoRequest.Page < 1) {
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
    }
}