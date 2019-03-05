namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using TodoList.Core.Entities;

    public class Program
    {
        static void Main(string[] args)
        {
            Startup startup = new Startup();

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

    public sealed class Startup
    {
        DBContext inMemoryContext;
        ITodoItemGateway gateway;
        ConsoleUseCaseOutputHandler outputHandler;
        EntitiesFactory entitiesFactory;

        public Startup()
        {
            inMemoryContext = new DBContext();
            gateway = new TodoItemGateway(inMemoryContext);
            outputHandler = new ConsoleUseCaseOutputHandler();
            entitiesFactory = new EntitiesFactory();
        }

        public void UpdateTodoItem(string[] args)
        {
            if (args.Length != 3)
                return;

            Guid id;

            if (!Guid.TryParse(args[1], out id))
                return;

            var updateTodoItem = new Core.UseCases.UpdateTitle.Interactor(
                gateway
            );

            var input = new Core.UseCases.UpdateTitle.Input(id, args[2]);
            updateTodoItem.Execute(input);
        }

        public void ListTodoItem(string[] args)
        {
            if (args.Length != 1)
                return;

            var list = new Core.UseCases.ListTodoItems.Interactor(
                outputHandler,
                gateway
            );

            list.Execute(new Core.UseCases.ListTodoItems.Input());
        }

        public void FinishTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            IUseCase<Guid> finish = new Core.UseCases.FinishTodoItem.Interactor(
                gateway
            );

            finish.Execute(new Guid(args[1]));
        }

        public void AddTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            IUseCase<Core.UseCases.AddTodoItem.Input> addTodoItem = new Core.UseCases.AddTodoItem.Interactor(
                outputHandler,
                gateway,
                entitiesFactory
            );

            var input = new Core.UseCases.AddTodoItem.Input(args[1]);
            addTodoItem.Execute(input);
        }
    }

    public class ConsoleUseCaseOutputHandler:
        IOutputHandler<Core.UseCases.AddTodoItem.Output>,
        IOutputHandler<Core.UseCases.ListTodoItems.Output>
    {
        public void Handle(Core.UseCases.AddTodoItem.Output output)
        {
            Console.WriteLine($"Added {output.Id}.");
        }

        public void Handle(Core.UseCases.ListTodoItems.Output output)
        {
            foreach (var item in output.Items)
                Console.WriteLine($"{item.Id} - {item.Title}.");
        }
    }
}