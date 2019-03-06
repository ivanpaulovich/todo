namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.UseCases;

    public sealed class Presenter: 
        IOutputHandler<Core.UseCases.AddTodoItem.AddTodoItemResponse>,
        IOutputHandler<Core.UseCases.ListTodoItems.ListTodoItemsResponse>
    {
        public void Handle(Core.UseCases.AddTodoItem.AddTodoItemResponse output)
        {
            Console.WriteLine($"Added {output.Id}.");
        }

        public void Handle(Core.UseCases.ListTodoItems.ListTodoItemsResponse output)
        {
            foreach (var item in output.Items)
                Console.WriteLine($"{item.Id} - {item.Title}.");
        }
    }
}