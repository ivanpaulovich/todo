namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Boundaries.MarkItemCompleted;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class MarkItemIncompleted : IUseCase
    {
        private ITodoItemGateway _todoItemGateway;

        public MarkItemIncompleted(
            ITodoItemGateway todoItemGateway)
        {
            _todoItemGateway = todoItemGateway;
        }

        public void Execute(Guid itemId)
        {
            ITodoItem todoItem = _todoItemGateway.Get(itemId);
            todoItem.MarkIncomplete();
            _todoItemGateway.Update(todoItem);
        }
    }
}