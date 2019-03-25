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
            var finish = new Core.UseCases.RemoveTodoItem(gateway);
            var add = new Core.UseCases.AddTodoItem(presenter, gateway, entitiesFactory);
            var markCompleted = new Core.UseCases.MarkItemCompleted(gateway);
            var markIncomplete = new Core.UseCases.MarkItemIncomplete(gateway);

            Startup startup = new Startup(add, finish, list, update, markCompleted, markIncomplete);
            presenter.DisplayInstructions();
            startup.ListTodoItem();

            while (true)
            {
                string command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command) || string.Compare(command, "exit", StringComparison.CurrentCultureIgnoreCase) == 0)
                    break;

                string[] input = command.Split(' ');

                if (string.Compare(input[0], "todo", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.AddTodoItem(command);

                if (string.Compare(input[0], "rm", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.RemoveTodoItem(input);

                if (string.Compare(input[0], "print", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.ListTodoItem();

                if (string.Compare(input[0], "rename", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.UpdateTodoItem(input, command);

                if (string.Compare(input[0], "do", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.CompleteTodoItem(input);

                if (string.Compare(input[0], "undo", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.IncompleteTodoItem(input);
            }
        }
    }
}