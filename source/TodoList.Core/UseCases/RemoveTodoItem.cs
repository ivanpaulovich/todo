namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Boundaries.RemoveTodoItem;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class RemoveTodoItem : IUseCase
    {
        private ITodoItemGateway _todoItemGateway;

        public RemoveTodoItem(
            ITodoItemGateway todoItemGateway)
        {
            _todoItemGateway = todoItemGateway;
        }

        public void Execute(Guid todoItemId)
        {
            _todoItemGateway.Delete(todoItemId);
        }
    }
}