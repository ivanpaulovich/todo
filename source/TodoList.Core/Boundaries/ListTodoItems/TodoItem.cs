namespace TodoList.Core.Boundaries.ListTodoItems
{
    using System;

    public sealed class TodoItem
    {
        public Guid Id { get; }
        public string Title { get; }
        public bool IsCompleted { get; }

        public TodoItem(Guid id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }
    }
}