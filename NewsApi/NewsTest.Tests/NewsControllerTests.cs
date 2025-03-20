using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using news_management.Controllers;
using news_management.Data;
using news_management.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace news_management.Tests
{
    public class NewsControllerUnitTests
    {
        private readonly ApplicationDbContext _context;
        private readonly NewsController _controller;

        public NewsControllerUnitTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "news_management")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _controller = new NewsController(_context);
        }

        [Fact]
    public async Task GetNews_ReturnsOrderedNews()
    {
        // Arrange
        _context.News.AddRange(new List<News>
        {
            new News { id = 1, title = "Test 1", content = "Content 1", date = new DateOnly(2023, 1, 2) },
            new News { id = 2, title = "Test 2", content = "Content 2", date = new DateOnly(2023, 1, 1) }
        });

        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetNews();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<News>>>(result);
        var returnedNews = Assert.IsType<List<News>>(actionResult.Value);
        Assert.Equal(1, returnedNews[0].id);
        Assert.Equal(2, returnedNews[1].id);
    }

        [Fact]
        public async Task CreateNews_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");
            var news = new News(
                    title: "Test 2",
                    content: "Content 2",
                    date: new DateOnly(2023, 1, 3)
                    );

            // Act
            var result = await _controller.CreateNews(news);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}