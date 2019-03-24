namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Boundaries.MarkItemIncomplete;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class MarkItemIncomplete : IUseCase
    {
        private ITodoItemGateway _todoItemGateway;

        public MarkItemIncomplete(
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