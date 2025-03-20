using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using news_management.Data;
using Xunit;

namespace news_management.Tests
{
    public class DataInitializerTests
    {
        // [Fact]
        // public async Task Initialize_ShouldMigrateDatabase()
        // {
        //     var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //         .UseSqlServer("Server=db,1433;Database=news_management;User Id=sa;Password=Pa55word1950;TrustServerCertificate=True;MultipleActiveResultSets=true")
        //         .Options;

        //     using var context = new ApplicationDbContext(options);
        //     var initializer = new DataInitializer(context);

        //     // Act
        //     await initializer.Initialize();

        //     // Assert
        //     Assert.True(await context.Database.CanConnectAsync());
        // }
        // [Fact]
        // public async Task Initialize_ShouldMigrateDatabase()
        // {
        //     // Arrange
        //     var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //         .UseSqlServer("Server=.;Database=Test;Trusted_Connection=True;TrustServerCertificate=true")
        //         .Options;

        //     var mockDbContext = new Mock<ApplicationDbContext>(options);
        //     var mockDatabase = new Mock<DatabaseFacade>(mockDbContext.Object);

        //     mockDbContext.SetupGet(db => db.Database).Returns(mockDatabase.Object);
        //     mockDatabase.Setup(d => d.MigrateAsync(It.IsAny<CancellationToken>()))
        //                  .Returns(Task.CompletedTask);

        //     var initializer = new DataInitializer(mockDbContext.Object);

        //     // Act
        //     await initializer.Initialize();

        //     // Assert
        //     mockDatabase.Verify(d => d.MigrateAsync(It.IsAny<CancellationToken>()), Times.Once);
        // }
    }
}