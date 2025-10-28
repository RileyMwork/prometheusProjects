using System.Net;
using System.Net.Http.Json;
using Xunit;
using project1.model;
using System.Reflection;

namespace project1.test.post {
    public class DeleteTests {
        private readonly HttpClient client;

        public DeleteTests() {
            client = new HttpClient{
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };
        }

        [Fact]
        public async Task deletePostReturnsSuccess()
        {
            var response = await client.DeleteAsync("posts/1");
            response.EnsureSuccessStatusCode();
        }
    }
}
