using Microsoft.EntityFrameworkCore;
using news_management.Data;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});
builder.WebHost.UseUrls("http://0.0.0.0:7117");
const string CorsDomains = "CorsDomains";


// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(
//         builder.Configuration.GetConnectionString("DefaultConnection"),
//         sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
//     ));

if (builder.Environment.IsDevelopment()) {
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("news_management");
        });
} else{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsDomains,
        policy =>
        {
            var origins = builder.Configuration.GetSection("HttpHeaders:CorsDomains")
                .GetChildren()
                .Select(c => c.Value)
                .Where(v => !string.IsNullOrEmpty(v))
                .ToArray() ?? Array.Empty<string>();

            policy.WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});


builder.Services.AddControllers();
builder.Services.AddScoped<DataInitializer>();
builder.Services.AddHttpClient();


var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(CorsDomains);
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var initializer = serviceProvider.GetRequiredService<DataInitializer>();

    await initializer.Initialize();
}


app.Run();

public partial class Program { }