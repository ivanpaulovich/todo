namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Boundaries.UpdateTitle;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class UpdateTitle : IUseCase<Request>
    {
        private ITodoItemGateway _todoItemGateway;

        public UpdateTitle(
            ITodoItemGateway todoItemGateway)
        {
            _todoItemGateway = todoItemGateway;
        }

        public void Execute(Request request)
        {
            if (request == null)
                throw new Exception("Input is null");

            if (string.IsNullOrWhiteSpace(request.Title))
                throw new Exception("Title is null");

            TodoItem todoItem = _todoItemGateway.Get(request.TodoItemId);
            todoItem.UpdateTitle(request.Title);
            _todoItemGateway.Update(todoItem);
        }
    }
}