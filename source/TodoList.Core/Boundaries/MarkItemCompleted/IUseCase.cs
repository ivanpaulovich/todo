namespace TodoList.Core.Boundaries.MarkItemCompleted
{
    using System;

    public interface IUseCase
    {
        void Execute(Guid todoItemId);
    }
}