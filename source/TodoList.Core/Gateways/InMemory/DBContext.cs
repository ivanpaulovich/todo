namespace TodoList.Core.Gateways.InMemory {
    using System.Collections.ObjectModel;
    using TodoList.Core.Entities;

    public sealed class DBContext {
        public Collection<TodoItem> TodoItems { get; set; }

        public DBContext () {
            TodoItems = new Collection<TodoItem> ();
        }
    }
}