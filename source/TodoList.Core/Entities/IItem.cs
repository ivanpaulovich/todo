namespace TodoList.Core.Entities
{
    using System;

    public interface IItem
    {
        Guid Id { get; }
        string Title { get; }
        bool Done { get; }
        void Rename(string title);
        void Do();
        void Undo();
    }
}