using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;


namespace Tests
{
    [TestFixture]
    public class AuthorIntegrationTests
    {
        private HttpClient _client;

        [TearDown]
        public void Teardown()
        {
            _client?.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            var appFactory = new WebApplicationFactory<Program>();
            _client = appFactory.CreateClient();
        }

        [Test]
        public async Task GetAuthorById_ReturnsAuthor()
        {
            var response = await _client.GetAsync("/api/author/1");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(content.Contains("Sample Author"));
        }
    }
}
