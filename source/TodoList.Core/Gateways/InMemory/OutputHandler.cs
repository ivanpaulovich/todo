namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TodoList.Core.UseCases;
    using TodoList.Core.UseCases.AddTodoItem;
    using TodoList.Core.UseCases.ListTodoItems;

    public sealed class OutputHandler :
        IOutputHandler<Core.UseCases.AddTodoItem.AddTodoItemResponse>,
        IOutputHandler<Core.UseCases.ListTodoItems.ListTodoItemsResponse>
    {
        public Collection<Core.UseCases.AddTodoItem.AddTodoItemResponse> AddTodoItems { get; }
        public Collection<Core.UseCases.ListTodoItems.ListTodoItemsResponse> ListTodoItems { get; }

        public OutputHandler()
        {
            AddTodoItems = new Collection<Core.UseCases.AddTodoItem.AddTodoItemResponse>();
            ListTodoItems = new Collection<Core.UseCases.ListTodoItems.ListTodoItemsResponse>();
        }

        public void Handle(Core.UseCases.AddTodoItem.AddTodoItemResponse output)
        {
            AddTodoItems.Add(output);
        }

        public void Handle(UseCases.ListTodoItems.ListTodoItemsResponse output)
        {
            ListTodoItems.Add(output);
        }
    }
}