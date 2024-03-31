using Microsoft.AspNetCore.Mvc;
using StackOverflowTagsAPI.Models.StackOverflow;

namespace StackOverflowTagsAPI.Controllers {
    [ApiController]
    [Route("Tags")]
    public class TagsController(ILogger<TagsController> logger) : ControllerBase {
        private readonly ILogger<TagsController> Logger = logger;

        [HttpGet]
        [Route("GetTags")]
        public IEnumerable<Tag> GetTags() {
            return new List<Tag>();
        }
    }
}