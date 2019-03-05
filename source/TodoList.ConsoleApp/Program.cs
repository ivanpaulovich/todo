namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;

    public class Program
    {
        static void Main(string[] args)
        {
            Startup startup = new Startup();

            while (true)
            {
                string command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command))
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

        public Startup()
        {
            inMemoryContext = new DBContext();
            gateway = new TodoItemGateway(inMemoryContext);
            outputHandler = new ConsoleUseCaseOutputHandler();
        }

        public void UpdateTodoItem(string[] args)
        {
            var updateTodoItem = new Core.UseCases.UpdateTitle.Interactor(
                gateway
            );

            var input = new Core.UseCases.UpdateTitle.Input(new Guid(args[1]), args[2]);
            updateTodoItem.Execute(input);
        }

        public void ListTodoItem(string[] args)
        {
            var list = new Core.UseCases.ListTodoItems.Interactor(
                outputHandler,
                gateway
            );

            list.Execute(new Core.UseCases.ListTodoItems.Input());
        }

        public void FinishTodoItem(string[] args)
        {
            IUseCase<Guid> finish = new Core.UseCases.FinishTodoItem.Interactor(
                gateway
            );

            finish.Execute(new Guid(args[1]));
        }

        public void AddTodoItem(string[] args)
        {
            IUseCase<Core.UseCases.AddTodoItem.Input> addTodoItem = new Core.UseCases.AddTodoItem.Interactor(
                outputHandler,
                gateway
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
                    Console.WriteLine($"Item {item.Id} - {item.Title}.");
            }
        }
}