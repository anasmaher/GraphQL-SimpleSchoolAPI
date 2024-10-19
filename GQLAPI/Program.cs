using GQLDomain.Database;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GQLAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Retrieve connection string
            string? connectionString = builder.Configuration.GetConnectionString("Default");

            // Register services
            builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register GraphQL server and queries
            builder.Services.AddGraphQLServer().AddQueryType<Query>();

            // Build the app
            var app = builder.Build();

            // Run migrations
            using (var scope = app.Services.CreateScope())
            {
                var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
                using (var context = contextFactory.CreateDbContext())
                {
                    context.Database.Migrate();
                }
            }

            // Configure middleware and route mappings
            app.MapGraphQL();

            // Run the app
            app.Run();
        }
    }
}
