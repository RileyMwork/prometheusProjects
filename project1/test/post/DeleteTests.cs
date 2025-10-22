using System.Net;
using System.Net.Http.Json;
using Xunit;
using project1.model;
using System.Reflection;

namespace project1.test.post {
    public class DeleteTests {
        private readonly HttpClient _client;

        public DeleteTests() {
            _client = new HttpClient{
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };
        }

        [Fact]
        public async Task deletePostReturnsSuccess()
        {
            var response = await _client.DeleteAsync("posts/1");
            response.EnsureSuccessStatusCode();
        }
    }
}
