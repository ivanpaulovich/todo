namespace TodoList.Core.UseCases.ListTodoItems
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class ListTodoItemsResponse
    {
        public IReadOnlyCollection<TodoItem> Items { get; }

        public ListTodoItemsResponse(IList<TodoItem> items)
        {
            Items = new ReadOnlyCollection<TodoItem>(items);
        }
    }
}