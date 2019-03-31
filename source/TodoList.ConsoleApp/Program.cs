namespace TodoList.ConsoleApp
{
    using System.Collections.Generic;
    using System;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.ConsoleApp.Commands;

    public class Program
    {
        static void Main(string[] args)
        {
            CommandParser commandParser = new CommandParser();
            ICommand command = commandParser.ParseCommand(args);
            Startup startup = new Startup();
            startup.ConfigureServices();
            startup.Run(command);
        }
    }
}