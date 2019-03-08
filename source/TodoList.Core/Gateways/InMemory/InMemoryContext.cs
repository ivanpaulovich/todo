namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.ObjectModel;
    using TodoList.Core.Entities;

    public sealed class InMemoryContext
    {
        public Collection<TodoItem> TodoItems { get; set; }

        public InMemoryContext()
        {
            TodoItems = new Collection<TodoItem>();
        }
    }
}