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
        static void Main(string[] args)
        {
            DBContext inMemoryContext = new DBContext();
            ITodoItemGateway gateway = new TodoItemGateway(inMemoryContext);
            var outputHandler = new ConsoleUseCaseOutputHandler();

            IUseCase<Core.UseCases.AddTodoItem.Input> addTodoItem = new Core.UseCases.AddTodoItem.Interactor(
                outputHandler,
                gateway
            );

            var input = new Core.UseCases.AddTodoItem.InputBuilder();
            input.WithTitle("My Task");

            addTodoItem.Execute(input.Build());
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
