namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core;
    using TodoList.Core.Gateways;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.UseCases;
    using TodoList.Core.UseCases.AddTodoItem;

    class Program
    {
        static DBContext inMemoryContext = new DBContext();
        static ITodoItemGateway gateway = new TodoItemGateway(inMemoryContext);
        static ConsoleUseCaseOutputHandler outputHandler = new ConsoleUseCaseOutputHandler();

        static void Main(string[] args)
        {
            while (true)
            {
                string command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command))
                    break;

                string[] input = command.Split(' ');

                if (string.Compare(input[0], "a", StringComparison.CurrentCultureIgnoreCase) == 0)
                    AddTodoItem(input);

                if (string.Compare(input[0], "c", StringComparison.CurrentCultureIgnoreCase) == 0)
                    CompleteTodoItem(input);

                if (string.Compare(input[0], "l", StringComparison.CurrentCultureIgnoreCase) == 0)
                    ListTodoItem(input);

                if (string.Compare(input[0], "l", StringComparison.CurrentCultureIgnoreCase) == 0)
                    UpdateTodoItem(input);
            }
        }

        private static void UpdateTodoItem(string[] input)
        {
            IUseCase<Core.UseCases.UpdateTitle.Input> updateTodoItem = new Core.UseCases.UpdateTitle.Interactor(
                gateway
            );

            var builder = new Core.UseCases.UpdateTitle.InputBuilder();
            builder.WithTodoItemId(new Guid(input[1]));
            builder.WithTitle(input[2]);
            updateTodoItem.Execute(builder.Build());
        }

        private static void ListTodoItem(string[] input)
        {
            throw new NotImplementedException();
        }

        private static void CompleteTodoItem(string[] input)
        {
            throw new NotImplementedException();
        }

        static void AddTodoItem(string[] input)
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

    public class ConsoleUseCaseOutputHandler : IUseCaseOutputHandler<Core.UseCases.AddTodoItem.Output>
    {
        void IUseCaseOutputHandler<Core.UseCases.AddTodoItem.Output>.Handle(Output output)
        {
            Console.WriteLine($"Added {output.Id}.");
        }
    }
}
