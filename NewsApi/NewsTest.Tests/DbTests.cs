// public class DbTests : IClassFixture<WebApplicationFactory<Startup>>
// {
//     private readonly HttpClient _client;
//     private readonly WebApplicationFactory<Startup> _factory;

//     public DbTests(WebApplicationFactory<Startup> factory)
//     {
//         _factory = factory;
//         _client = _factory.CreateClient();

//         // Remplace la configuration par une base de données en mémoire
//         _factory.WithWebHostBuilder(builder =>
//         {
//             builder.ConfigureServices(services =>
//             {
//                 var descriptor = services.SingleOrDefault(d =>
//                     d.ServiceType ==
//                     typeof(DbContextOptions<MyDbContext>));

//                 services.Remove(descriptor);

//                 services.AddDbContext<MyDbContext>(options =>
//                 {
//                     options.UseInMemoryDatabase("news_management");
//                 });
//             });
//         });
//     }
    
//     // Tests ici
// }
