namespace TodoList.Core.Entities
{
    using System;

    public class Item : IItem
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual bool Done { get; protected set; }

        public Item() => Id = Guid.NewGuid();

        public void Rename(string title)
        {
            Title = title;
        }

        public void Do()
        {
            Done = true;
        }

        public void Undo()
        {
            Done = false;
        }

        public static IItem Restore(Guid itemId, string title, bool done)
        {
            Item restoredTodoItem = new Item();
            restoredTodoItem.Id = itemId;
            restoredTodoItem.Title = title;
            restoredTodoItem.Done = done;
            return restoredTodoItem;
        }
    }
}