namespace TodoList.Core.Boundaries.ListTodoItems
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class Response
    {
        public IReadOnlyCollection<TodoItem> Items { get; }

        public Response(IList<TodoItem> items)
        {
            Items = new ReadOnlyCollection<TodoItem>(items);
        }
    }
}