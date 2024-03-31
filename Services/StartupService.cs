using StackOverflowTagsAPI.Services.Interfaces;

namespace StackOverflowTagsAPI.Services {
    public class StartupService(ITagService tagService) : BackgroundService {
        private ITagService TagService { get; } = tagService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            await TagService.SetTags(stoppingToken);
        }
    }
}
