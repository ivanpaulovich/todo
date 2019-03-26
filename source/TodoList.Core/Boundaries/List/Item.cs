namespace TodoList.Core.Boundaries.List
{
    using System;

    public sealed class Item
    {
        public Guid ItemId { get; }
        public string Title { get; }
        public bool Done { get; }

        public Item(Guid itemId, string title, bool done)
        {
            ItemId = itemId;
            Title = title;
            Done = done;
        }
    }
}