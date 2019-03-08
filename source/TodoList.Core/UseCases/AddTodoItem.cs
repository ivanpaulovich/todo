namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;
    using TodoList.Core.Boundaries.AddTodoItem;
    using TodoList.Core.Boundaries;

    public sealed class AddTodoItem : IUseCase<Request>
    {
        private IResponseHandler<Response> _outputHandler;
        private ITodoItemGateway _todoItemGateway;
        private IEntitiesFactory _entitiesFactory;

        public AddTodoItem(
            IResponseHandler<Response> outputHandler,
            ITodoItemGateway todoItemGateway,
            IEntitiesFactory entitiesFactory)
        {
            _outputHandler = outputHandler;
            _todoItemGateway = todoItemGateway;
            _entitiesFactory = entitiesFactory;
        }

        public void Execute(Request request)
        {
            if (request == null)
                throw new Exception("Request is null");

            if (string.IsNullOrWhiteSpace(request.Title))
                throw new Exception("Title is null");

            TodoItem todoItem = _entitiesFactory.NewTodoItem(request.Title);
            _todoItemGateway.Add(todoItem);

            Response response = new Response(todoItem.Id);
            _outputHandler.Handle(response);
        }
    }
}