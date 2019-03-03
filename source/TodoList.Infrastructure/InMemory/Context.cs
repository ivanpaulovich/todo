namespace TodoList.Infrastructure.InMemory {
    public sealed class Context {
        public Collection<TodoItem> TodoItems { get; set; }

        public Context () {
            TodoItems = new Collection<TodoItem> ();
        }
    }
}