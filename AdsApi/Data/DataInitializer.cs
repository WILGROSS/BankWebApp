using AdsApi.Data;
using Microsoft.EntityFrameworkCore;

public class DataInitializer
{
    private readonly IServiceProvider _serviceProvider;

    public DataInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void InitializeDatabase()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AdsApiContext>();
            dbContext.Database.Migrate(); // Applies any pending migrations and creates the database if it does not exist
        }
    }
}
