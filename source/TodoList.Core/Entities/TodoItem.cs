namespace TodoList.Core.Entities
{
    using System;

    public class TodoItem
    {
        public virtual Guid Id { get; private set; }
        public virtual string Title { get; private set; }

        public TodoItem(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }

        internal void UpdateTitle(string title)
        {
            Title = title;
        }
    }
}