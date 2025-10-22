using System.Net;
using System.Net.Http.Json;
using Xunit;
using project1.model;
using System.Reflection;

namespace project1.test.post {
    public class GetTests {
        private readonly HttpClient _client;

        public GetTests() {
            _client = new HttpClient{
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };
        }

        [Fact]
        public async Task getAllPostsReturnsSuccessAndList() {
            var response = await _client.GetAsync("posts");
            response.EnsureSuccessStatusCode();

            var posts = await response.Content.ReadFromJsonAsync<List<Post>>();
            Assert.NotNull(posts);
            Assert.NotEmpty(posts);
            Assert.True(posts.Count >= 2);
        }

        [Fact]
        public async Task getPostByIdReturnsSuccessAndCorrectId()
        {
            var response = await _client.GetAsync("posts/1");
            response.EnsureSuccessStatusCode();

            var post = await response.Content.ReadFromJsonAsync<Post>();
            Assert.NotNull(post);
            Assert.Equal(1, post.Id);
        }

        [Fact]
        public async Task negativeTestGetPostByIdEquals0ReturnsNotFound()
        {
            var response = await _client.GetAsync("posts/0");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
