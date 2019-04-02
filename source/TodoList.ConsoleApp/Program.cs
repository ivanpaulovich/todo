namespace TodoList.ConsoleApp
{
    using System.Collections.Generic;
    using System.IO;
    using System;
    using Microsoft.Extensions.Configuration;
    using TodoList.ConsoleApp.Commands;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;

    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional : false, reloadOnChange : true);

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