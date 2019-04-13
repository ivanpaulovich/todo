namespace TodoList.ConsoleApp
{
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using Colorful;
    using Microsoft.Extensions.Configuration;
    using TodoList.Core.Exceptions;

    public sealed class Program
    {
        static void Main(string[] args)
        {
            try
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

                if (configuration["Environment"] == "Cloud")
                    startup.ConfigureGistServices();

                startup.Run(args);
            }
            catch(BusinessException ex)
            {
                Console.WriteLine(ex.Message, Color.Yellow);
            }
            catch(InfrastructureException ex)
            {
                Console.WriteLine(ex.Message, Color.Red);
            }
        }
    }
}