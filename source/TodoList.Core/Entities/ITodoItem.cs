namespace TodoList.Core.Entities
{
    using System;

    public interface ITodoItem
    {
        Guid Id { get; }
        string Title { get; }
        bool IsCompleted { get; }
        void UpdateTitle(string title);
        void MarkCompleted();
        void MarkIncomplete();
    }
}