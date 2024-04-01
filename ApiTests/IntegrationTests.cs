using StackOverflowTagsAPI.Models.API;
using StackOverflowTagsAPI.Models.StackOverflow;
using StackOverflowTagsAPI.Services.Interfaces;
using System.Net;

namespace ApiTests {
    public class IntegrationTests : IClassFixture<ServiceCollectionFixture>, IDisposable {
        private const string StackOverflowAPI = "https://api.stackexchange.com/";
        private readonly HttpClient HttpClient;
        private readonly IHttpService HttpService;
        private readonly IStorageService StorageService;

        public IntegrationTests(ServiceCollectionFixture fixture) {
            var httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            HttpClient = new HttpClient(httpClientHandler);
            HttpService = (IHttpService)fixture.ServiceProvider.GetService(typeof(IHttpService));
            StorageService = (IStorageService)fixture.ServiceProvider.GetService(typeof(IStorageService));
            StorageService.SaveTags([new TagInfo { Count = 1, Name = "IntegrationTest", PercentageShare = 100.0m }]);
        }

        [Fact]
        public async Task IsStackOverflowAPIWorking_ShouldReturnTrue() {
            var response = await HttpClient.GetAsync(new Uri($"{StackOverflowAPI}info?site=stackoverflow"));
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task IsHttpServiceWorking_ShouldReturnTrue() {
            var response = await HttpService.SendApiRequest<InfoResponse>(HttpMethod.Get, "info?site=stackoverflow");
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task AreAnyTagsCached_ShouldReturnTrue() {
            var isSuccess = false;
            try {
                var tags = await StorageService.GetTags();
                isSuccess = tags.Any();
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
            Assert.True(isSuccess);
        }

        public void Dispose() {
            HttpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
