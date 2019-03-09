namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Boundaries.FinishTodoItem;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class FinishTodoItem : IUseCase
    {
        private ITodoItemGateway _todoItemGateway;

        public FinishTodoItem(
            ITodoItemGateway todoItemGateway)
        {
            _todoItemGateway = todoItemGateway;
        }

        public void Execute(Guid todoItemId)
        {
            ITodoItem todoItem = _todoItemGateway.Get(todoItemId);
            _todoItemGateway.Delete(todoItem);
        }
    }
}