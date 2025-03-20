using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using news_management;
using news_management.Data;
using news_management.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace news_management.Tests
{
    public class NewsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ApplicationDbContext _context;
        private readonly string _databaseName;

        public NewsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _databaseName = Guid.NewGuid().ToString();

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(_databaseName)
                               .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                    });
                });
            });

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(_databaseName)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new ApplicationDbContext(options);
        }

        private void CleanDatabase()
        {
            _context.News.RemoveRange(_context.News);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetNews_ReturnsSuccess()
        {
            CleanDatabase();

            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/news");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNewsDoesNotExist()
        {
            CleanDatabase();

            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.GetAsync("/api/news/1");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateNews_ReturnsCreatedResponse()
        {
            CleanDatabase();

            // Arrange
            var client = _factory.CreateClient();
            var newNews = new News
            {
                title = "Test News",
                content = "Test Content",
                date = new DateOnly(2023, 1, 2)
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/news", newNews);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }
        [Fact]
        public async Task PostNews_CreatesNewItem()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newNews = new News
            {
                title = "Test Title",
                content = "Test Content"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/news", newNews);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdNews = await response.Content.ReadFromJsonAsync<News>();
            Assert.NotNull(createdNews);
            Assert.Equal("Test Title", createdNews.title);

        }
        [Fact]
        public async Task DatabaseInitialization_ShouldWork()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                var initializer = scopedServices.GetRequiredService<DataInitializer>();

                var newsBefore = await db.News.ToListAsync();
                Assert.Empty(newsBefore);

                await initializer.Initialize();

                var newsAfter = await db.News.ToListAsync();
                Assert.Empty(newsAfter);
            }
        }
    }
}