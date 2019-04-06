namespace TodoList.ConsoleApp
{
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        static void Main(string[] args)
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var builder = new ConfigurationBuilder()
                .SetBasePath(assemblyFolder)
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();

            Startup startup = new Startup(configuration);

            if (configuration["Environment"] == "Development")
                startup.ConfigureInMemoryServices();

            if (configuration["Environment"] == "Staging")
                startup.ConfigureFileSystemServices();

            if (configuration["Environment"] == "Production")
                startup.ConfigureSqlServerServices();

            startup.Run(args);
        }
    }
}