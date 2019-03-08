namespace TodoList.Core.Boundaries.FinishTodoItem
{
    using System;

    public interface IUseCase
    {
        void Execute(Guid todoItemId);
    }
}