namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;

    public class Program
    {
        static void Main(string[] args)
        {
            InMemoryContext inMemoryContext = new InMemoryContext();
            ITodoItemGateway gateway = new TodoItemGateway(inMemoryContext);
            EntitiesFactory entitiesFactory = new EntitiesFactory();
            Presenter presenter = new Presenter();

            var update = new Core.UseCases.UpdateTitle(gateway);
            var list = new Core.UseCases.ListTodoItems(presenter, gateway);
            var finish = new Core.UseCases.FinishTodoItem(gateway);
            var add = new Core.UseCases.AddTodoItem(presenter, gateway, entitiesFactory);

            Startup startup = new Startup(add, finish, list, update);

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