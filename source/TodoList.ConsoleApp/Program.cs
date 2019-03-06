namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;
    using TodoList.Core.Gateways.InMemory;

    public class Program
    {
        static void Main(string[] args)
        {
            DBContext inMemoryContext = new DBContext();
            ITodoItemGateway gateway = new TodoItemGateway(inMemoryContext);
            EntitiesFactory entitiesFactory = new EntitiesFactory();
            Presenter presenter = new Presenter();
            Startup startup = new Startup(gateway, entitiesFactory, presenter);

            Console.WriteLine("Usage:\n\tadd [title]\n\tfinish [id]\n\tlist\n\tupdate [id] [title]\n\texit");

            while (true)
            {
                string command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command) || string.Compare(command, "exit", StringComparison.CurrentCultureIgnoreCase) == 0)
                    break;

                string[] input = command.Split(' ');

                if (string.Compare(input[0], "add", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.AddTodoItem(input);

                if (string.Compare(input[0], "finish", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.FinishTodoItem(input);

                if (string.Compare(input[0], "list", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.ListTodoItem(input);

                if (string.Compare(input[0], "update", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.UpdateTodoItem(input);
            }
        }
    }
}