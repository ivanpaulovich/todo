namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.UseCases;

    public sealed class Presenter: 
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