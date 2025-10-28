using System.Net;
using System.Net.Http.Json;
using Xunit;
using project1.model;

namespace project1.test.post {
    public class PostTests
    {
        private readonly HttpClient client;

        public PostTests()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };
        }

        [Fact]
        public async Task newPostReturnsCreatedStatusAndMatchingData()
        {

            var newPost = new Post
            {
                UserId = 1,
                Title = "Test Title",
                Body = "Test Body"
            };

            var response = await client.PostAsJsonAsync("posts", newPost);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdPost = await response.Content.ReadFromJsonAsync<Post>();
            Assert.NotNull(createdPost);
            Assert.Equal(newPost.Title, createdPost.Title);
            Assert.Equal(newPost.Body, createdPost.Body);
            Assert.Equal(newPost.UserId, createdPost.UserId);
            Assert.True(createdPost.Id > 0);
        }
    }
}
