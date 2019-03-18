namespace TodoList.Core.Boundaries.MarkItemIncomplete
{
    using System;

    public interface IUseCase
    {
        void Execute(Guid todoItemId);
    }
}