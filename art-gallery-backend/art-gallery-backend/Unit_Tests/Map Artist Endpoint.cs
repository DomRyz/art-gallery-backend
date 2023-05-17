using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace YourNamespace.Tests
{
    public class ArtistsControllerTests : IClassFixture<WebApplicationFactory<YourStartupClass>>
    {
        private readonly WebApplicationFactory<YourStartupClass> _factory;

        public ArtistsControllerTests(WebApplicationFactory<YourStartupClass> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetArtists_ReturnsOkResult()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/artists/");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetArtistById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var client = _factory.CreateClient();
            var existingArtistId = 1;

            // Act
            var response = await client.GetAsync($"/api/artists/{existingArtistId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetArtistById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var client = _factory.CreateClient();
            var nonExistingArtistId = 999;

            // Act
            var response = await client.GetAsync($"/api/artists/{nonExistingArtistId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Similarly, you can write tests for other endpoints like GetArtistOfTheDay, InsertArtist, UpdateArtist, and DeleteArtist.
        // Use the appropriate HTTP methods (GET, POST, PUT, DELETE) and provide the necessary request body or parameters.

        // Remember to test both the success scenarios (e.g., valid input) and failure scenarios (e.g., invalid input, database errors).

        // You can also test the authorization by including the required headers or cookies in the HttpClient requests.
    }
}
