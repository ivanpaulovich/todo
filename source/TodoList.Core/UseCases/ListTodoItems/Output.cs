namespace TodoList.Core.UseCases.ListTodoItems
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class Output
    {
        public IReadOnlyCollection<TodoItem> Items { get; }

        public Output(IList<TodoItem> items)
        {
            Items = new ReadOnlyCollection<TodoItem>(items);
        }
    }
}