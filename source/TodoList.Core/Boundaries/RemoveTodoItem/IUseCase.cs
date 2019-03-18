namespace TodoList.Core.Boundaries.RemoveTodoItem
{
    using System;

    public interface IUseCase
    {
        void Execute(Guid todoItemId);
    }
}