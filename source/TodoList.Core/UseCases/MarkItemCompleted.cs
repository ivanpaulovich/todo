namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Boundaries.MarkItemCompleted;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class MarkItemCompleted : IUseCase
    {
        private ITodoItemGateway _todoItemGateway;

        public MarkItemCompleted(
            ITodoItemGateway todoItemGateway)
        {
            _todoItemGateway = todoItemGateway;
        }

        public void Execute(Guid itemId)
        {
            ITodoItem todoItem = _todoItemGateway.Get(itemId);
            todoItem.Complete();
            _todoItemGateway.Update(todoItem);
        }
    }
}