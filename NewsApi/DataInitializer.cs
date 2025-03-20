using Microsoft.EntityFrameworkCore;
using news_management.Data;

public class DataInitializer
{
    private readonly ApplicationDbContext _db;

    public DataInitializer(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task Initialize()
    {
        if (_db.Database.IsRelational())
        {
            await _db.Database.MigrateAsync();
        }
        else
        {
            await _db.Database.EnsureCreatedAsync();
        }
    }
}