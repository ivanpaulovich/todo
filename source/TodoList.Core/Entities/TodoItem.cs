namespace TodoList.Core.Entities
{
    using System;

    public class TodoItem : ITodoItem
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual bool IsCompleted { get; protected set; }

        public TodoItem()
        {
            Id = Guid.NewGuid();
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public static TodoItem Restore(Guid id, string title)
        {
            TodoItem restoredTodoItem = new TodoItem();
            restoredTodoItem.Id = id;
            restoredTodoItem.Title = title;
            return restoredTodoItem;
        }

        public void MarkCompleted()
        {
            IsCompleted = true;
        }

        public void MarkIncomplete()
        {
            IsCompleted = false;
        }
    }
}