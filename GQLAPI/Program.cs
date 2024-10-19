using GQLDomain.Database;
using Microsoft.EntityFrameworkCore;

namespace GQLAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? ConnectionString = builder.Configuration.GetConnectionString("Default");
            
            // Register services before building the app
            //builder.Services.AddGraphQLServer().AddQueryType<Query>();

            builder.Services.AddPooledDbContextFactory<AppDbContext>(o => o.UseSqlServer(ConnectionString));


            var app = builder.Build();

            // Configure middleware and route mappings
            app.MapGraphQL();

            app.Run();
        }
    }
}
