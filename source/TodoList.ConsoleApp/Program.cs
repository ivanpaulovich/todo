namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using System.Collections.Generic;
    using TodoList.Core.Boundaries;

    /// <summary>
    /// My to do list app
    /// </summary>
    public class Program
    {
        /// <summary>
        /// My to do list app
        /// </summary>
        /// <param name="commandType">The command types [todo, do, undo, rename, remove]</param>
        /// <param name="id">The item id</param>
        /// <param name="title">The item title</param>
        static void Main(CommandType commandType = CommandType.Todo, string id = "", string title = "aaaa")
        {
            Startup startup = new Startup();
            startup.ConfigureServices();
            startup.Run(commandType, id, title);
        }
    }
}