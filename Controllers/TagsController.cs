using Microsoft.AspNetCore.Mvc;
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
        [Route("GetTags")]
        public async Task<ApiResponse> GetTags(CancellationToken cancellationToken) {
            return await TagService.GetTags(cancellationToken);
        }
    }
}