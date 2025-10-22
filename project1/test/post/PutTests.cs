using System.Net;
using System.Net.Http.Json;
using Xunit;
using project1.model;

namespace project1.test.post {
    public class PutTests
    {
        private readonly HttpClient _client;

        public PutTests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };
        }

        [Fact]
        public async Task editedPostReturnsOkAndMatchingData()
        {

            var newPost = new Post
            {
                Id = 1,
                UserId = 1,
                Title = "Edited Test Title",
                Body = "Edited Test Body"
            };

            var response = await _client.PutAsJsonAsync("posts/1", newPost);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var createdPost = await response.Content.ReadFromJsonAsync<Post>();
            Assert.NotNull(createdPost);
            Assert.Equal(newPost.Id, createdPost.Id);
            Assert.Equal(newPost.Title, createdPost.Title);
            Assert.Equal(newPost.Body, createdPost.Body);
            Assert.Equal(newPost.UserId, createdPost.UserId);
            Assert.True(createdPost.Id > 0);
        }
    }
}
