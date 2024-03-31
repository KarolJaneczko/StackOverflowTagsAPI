using Newtonsoft.Json;
using StackOverflowTagsAPI.Models.Logic;
using StackOverflowTagsAPI.Services.Interfaces;
using System.Net;
using System.Text;

namespace StackOverflowTagsAPI.Services {
    public class HttpService : IHttpService {
        private readonly HttpClient httpClient;
        private readonly string apiAddress;

        public HttpService() {
            // Odpowiedzi ze StackOverflow są kompresowane metodą GZip.
            var httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClient = new HttpClient(httpClientHandler);
            apiAddress = "https://api.stackexchange.com/";
        }

        public async Task<ApiResponse> SendApiRequest<T>(HttpMethod httpMethod, string path, object body = null, CancellationToken cancellationToken = default) {
            try {
                var serializedBody = body is null ? "" : JsonConvert.SerializeObject(body);
                var request = new HttpRequestMessage {
                    Method = httpMethod,
                    RequestUri = new Uri(string.Concat(apiAddress, path)),
                    Content = httpMethod == HttpMethod.Get ? null : new StringContent(serializedBody, Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request, cancellationToken);
                if (response.StatusCode == HttpStatusCode.OK) {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken);
                    var data = JsonConvert.DeserializeObject<T>(content);
                    return new ApiResponse(true, data);
                } else {
                    throw new ApiException(response.StatusCode.ToString(), await response.Content.ReadAsStringAsync(cancellationToken));
                }
            } catch (ApiException ex) {
                return new ApiResponse(ex);
            } catch (Exception ex) {
                return new ApiResponse(ex);
            }
        }
    }
}
