namespace TodoList.Core.UseCases.FinishTodoItem {
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class Interactor : IUseCase<Guid> {
        private ITodoItemGateway _todoItemGateway;

        public Interactor (
            ITodoItemGateway todoItemGateway) {
            _todoItemGateway = todoItemGateway;
        }

        public void Execute (Guid todoItemId) {
            TodoItem todoItem = _todoItemGateway.Get (todoItemId);
            _todoItemGateway.Delete (todoItem);
        }
    }
}