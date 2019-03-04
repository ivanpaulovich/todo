namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core;
    using TodoList.Core.Gateways;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.UseCases;

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

        public void UpdateTodoItem(string[] input)
        {
            IUseCase<Core.UseCases.UpdateTitle.Input> updateTodoItem = new Core.UseCases.UpdateTitle.Interactor(
                gateway
            );

            var builder = new Core.UseCases.UpdateTitle.InputBuilder();
            builder.WithTodoItemId(new Guid(input[1]));
            builder.WithTitle(input[2]);
            updateTodoItem.Execute(builder.Build());
        }

        public void ListTodoItem(string[] input)
        {
            IUseCase list = new Core.UseCases.ListTodoItems.Interactor(
                outputHandler,
                gateway
            );

            list.Execute();
        }

        public void FinishTodoItem(string[] input)
        {
            IUseCase<Guid> finish = new Core.UseCases.FinishTodoItem.Interactor(
                gateway
            );

            finish.Execute(new Guid(input[1]));
        }

        public void AddTodoItem(string[] input)
        {
            IUseCase<Core.UseCases.AddTodoItem.Input> addTodoItem = new Core.UseCases.AddTodoItem.Interactor(
                outputHandler,
                gateway
            );

            var builder = new Core.UseCases.AddTodoItem.InputBuilder();
            builder.WithTitle(input[1]);
            addTodoItem.Execute(builder.Build());
        }
    }

    public class ConsoleUseCaseOutputHandler : 
        IUseCaseOutputHandler<Core.UseCases.AddTodoItem.Output>,
        IUseCaseOutputHandler<Core.UseCases.ListTodoItems.Output>
    {
        public void Handle(Core.UseCases.AddTodoItem.Output output)
        {
            Console.WriteLine($"Added {output.Id}.");
        }

        public void Handle(Core.UseCases.ListTodoItems.Output output)
        {
            foreach(var item in output.Items)
                Console.WriteLine($"Item {item.Id} - {item.Title}.");
        }
    }
}
