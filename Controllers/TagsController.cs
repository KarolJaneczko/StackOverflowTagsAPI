using Microsoft.AspNetCore.Mvc;
using StackOverflowTagsAPI.Models.API;
using StackOverflowTagsAPI.Models.Logic;
using StackOverflowTagsAPI.Services.Interfaces;

namespace StackOverflowTagsAPI.Controllers {
    [ApiController]
    [Route("Tags")]
    public class TagsController(ILogger<TagsController> logger, ITagService tagService) : ApiControllerBase<TagsController>(logger) {
        private ITagService TagService { get; } = tagService;

        [HttpGet]
        [Route("SetTags")]
        public async Task<ApiResponse> SetTags(CancellationToken cancellationToken) {
            async Task<ApiResponse> task() => await TagService.SetTags(cancellationToken);
            return await RunWithTryCatch(task);
        }

        [HttpPost]
        [Route("GetTagInfos")]
        public async Task<ApiResponse> GetTagInfos(TagInfoRequest request, CancellationToken cancellationToken) {
            async Task<ApiResponse> task() => await TagService.GetTagInfos(request, cancellationToken);
            return await RunWithTryCatch(task);
        }
    }
}