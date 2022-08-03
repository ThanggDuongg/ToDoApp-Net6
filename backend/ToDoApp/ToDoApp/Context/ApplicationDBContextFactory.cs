using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ToDoApp.Context
{
    public class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configurationRoot.GetConnectionString("DefaultConnection");

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new ApplicationDBContext(optionBuilder.Options);
        }
    }
}
