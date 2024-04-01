using StackOverflowTagsAPI.Models.Logic;
using StackOverflowTagsAPI.Services.Interfaces;

namespace StackOverflowTagsAPI.Services {
    public class StartupHostedService(ITagService tagService) : BackgroundService {
        private ITagService TagService { get; } = tagService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            try {
                await TagService.SetTags(stoppingToken);
            } catch (ApiException ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
