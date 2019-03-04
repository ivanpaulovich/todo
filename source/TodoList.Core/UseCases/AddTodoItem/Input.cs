namespace TodoList.Core.UseCases.AddTodoItem {
    public sealed class Input {
        internal Input () { }
        public string Title { get; internal set; }

        public Input (string title) {
            Title = title;
        }
    }
}