namespace TodoList.Infrastructure.FileSystemGateway
{
    using System;
    using TodoList.Core.Entities;

    public sealed class JsonItem : Item
    {
        public JsonItem(Guid id, string title, bool done)
        {
            Id = id;
            Title = title;
            Done = done;
        }
    }
}