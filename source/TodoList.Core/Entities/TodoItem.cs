namespace TodoList.Core.Entities {
    using System;

    public class TodoItem {
        public Guid Id { get; }
        public string Title { get; private set; }

        public TodoItem (string title) {
            Id = Guid.NewGuid ();
            Title = title;
        }

        internal void UpdateTitle (string title) {
            Title = title;
        }
    }
}