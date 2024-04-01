using Microsoft.Extensions.DependencyInjection;
using StackOverflowTagsAPI.Services;
using StackOverflowTagsAPI.Services.Interfaces;

namespace ApiTests {
    public class ServiceCollectionFixture {
        public IServiceCollection Services { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }

        public ServiceCollectionFixture() {
            Services = new ServiceCollection();
            Services.AddTransient<IStorageService, StorageService>();
            Services.AddTransient<IHttpService, HttpService>();
            ServiceProvider = Services.BuildServiceProvider();
        }
    }
}
