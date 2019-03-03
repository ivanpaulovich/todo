namespace TodoList.Core.Entities {
    using System;

    public class TodoItem {
        public Guid Id { get; }
        public string Title { get; }

        public TodoItem()
        {
            Id = Guid.NewGuid();
        }
    }
}