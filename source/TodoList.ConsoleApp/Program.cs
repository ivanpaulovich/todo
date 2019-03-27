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
            IItemGateway gateway = new ItemGateway(inMemoryContext);
            EntitiesFactory entitiesFactory = new EntitiesFactory();
            Presenter presenter = new Presenter();

            var renameUseCase = new Core.UseCases.Rename(gateway);
            var listUseCase = new Core.UseCases.List(presenter, gateway);
            var removeUseCase = new Core.UseCases.Remove(gateway);
            var todoUseCase = new Core.UseCases.Todo(presenter, gateway, entitiesFactory);
            var doUseCase = new Core.UseCases.Do(gateway);
            var undoUseCase = new Core.UseCases.Undo(gateway);

            Startup startup = new Startup(
                todoUseCase,
                removeUseCase,
                listUseCase,
                renameUseCase,
                doUseCase,
                undoUseCase);
                
            presenter.DisplayInstructions();
            startup.List();

            while (true)
            {
                string command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command) || string.Compare(command, "exit", StringComparison.CurrentCultureIgnoreCase) == 0)
                    break;

                string[] input = command.Split(' ');

                if (string.Compare(input[0], "todo", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.Todo(command);

                if (string.Compare(input[0], "rm", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.Remove(input);

                if (string.Compare(input[0], "ls", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.List();

                if (string.Compare(input[0], "ren", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.Rename(input, command);

                if (string.Compare(input[0], "do", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.Do(input);

                if (string.Compare(input[0], "undo", StringComparison.CurrentCultureIgnoreCase) == 0)
                    startup.Undo(input);
            }
        }
    }
}