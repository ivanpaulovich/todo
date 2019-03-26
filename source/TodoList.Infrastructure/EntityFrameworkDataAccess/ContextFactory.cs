namespace TodoList.Infrastructure.EntityFrameworkDataAccess
{
    using System.IO;
    using System;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public sealed class ContextFactory : IDesignTimeDbContextFactory<TodoContext>
    {
        public TodoContext CreateDbContext(string[] args)
        {
            string connectionString = ReadDefaultConnectionStringFromAppSettings();

            var builder = new DbContextOptionsBuilder<TodoContext>();
            builder.UseSqlServer(connectionString);
            return new TodoContext(builder.Options);
        }

        private string ReadDefaultConnectionStringFromAppSettings()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.production.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}