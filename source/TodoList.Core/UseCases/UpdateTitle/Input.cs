namespace TodoList.Core.UseCases.UpdateTitle {
    using System;

    public sealed class Input {
        public Input (Guid todoItemId, string title) { 
            TodoItemId = todoItemId;
            Title = title;
        }
        public Guid TodoItemId { get; internal set; }
        public string Title { get; internal set; }
    }
}