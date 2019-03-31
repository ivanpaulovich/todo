namespace TodoList.ConsoleApp
{
    using System.Collections.Generic;
    using System;
    using TodoList.ConsoleApp.Commands;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;

    public class Program
    {
        static void Main(string[] args)
        {
            Startup startup = new Startup();
            startup.ConfigureServices();
            startup.Run(args);
        }
    }
}