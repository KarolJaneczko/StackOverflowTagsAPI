using Microsoft.AspNetCore.Mvc;
using StackOverflowTagsAPI.Models.API;
using StackOverflowTagsAPI.Models.Logic;
using StackOverflowTagsAPI.Services.Interfaces;

namespace StackOverflowTagsAPI.Controllers {
    [ApiController]
    [Route("Tags")]
    public class TagsController : ControllerBase {
        private ILogger<TagsController> Logger { get; }
        private ITagService TagService { get; }

        public TagsController(ILogger<TagsController> logger, ITagService tagService) {
            Logger = logger;
            TagService = tagService;
        }

        [HttpGet]
        [Route("SetTags")]
        public async Task<ApiResponse> SetTags(CancellationToken cancellationToken) {
            return await TagService.SetTags(cancellationToken);
        }

        [HttpPost]
        [Route("GetTagInfos")]
        public async Task<ApiResponse> GetTagInfos(TagInfoRequest request, CancellationToken cancellationToken) {
            return await TagService.GetTagInfos(request, cancellationToken);
        }
    }
}