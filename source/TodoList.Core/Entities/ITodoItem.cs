namespace TodoList.Core.Entities
{
    using System;

    public interface ITodoItem
    {
        Guid Id { get; }
        string Title { get; }
        void UpdateTitle(string title);
    }
}