namespace TodoList.Core.Gateways.InMemory {
    using System.Collections.ObjectModel;
    using TodoList.Core.Entities;

    public sealed class Context {
        public Collection<TodoItem> TodoItems { get; set; }

        public Context () {
            TodoItems = new Collection<TodoItem> ();
        }
    }
}