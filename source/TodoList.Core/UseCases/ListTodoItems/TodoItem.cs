namespace TodoList.Core.UseCases.ListTodoItems {
    using System;

    public sealed class TodoItem {
        public Guid Id { get; }
        public string Title { get; }

        public TodoItem (Guid id, string title) {
            Id = id;
            Title = title;
        }
    }
}