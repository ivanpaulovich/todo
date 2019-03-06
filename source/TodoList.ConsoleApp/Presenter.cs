namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.UseCases.AddTodoItem;
    using TodoList.Core.UseCases.ListTodoItems;
    using TodoList.Core.UseCases;

    public sealed class Presenter : IOutputHandler<AddTodoItemResponse>, IOutputHandler<ListTodoItemsResponse>
    {
        public void Handle(AddTodoItemResponse response)
        {
            Console.WriteLine($"Added {response.Id}.");
        }

        public void Handle(ListTodoItemsResponse response)
        {
            foreach (var item in response.Items)
                Console.WriteLine($"{item.Id} - {item.Title}.");
        }
    }
}