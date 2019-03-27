namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Boundaries.Todo;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class Todo : IUseCase<Request>
    {
        private IResponseHandler<Response> _responseHandler;
        private IItemGateway _itemGateway;
        private IEntitiesFactory _entitiesFactory;

        public Todo(
            IResponseHandler<Response> responseHandler,
            IItemGateway itemGateway,
            IEntitiesFactory entitiesFactory)
        {
            _responseHandler = responseHandler;
            _itemGateway = itemGateway;
            _entitiesFactory = entitiesFactory;
        }

        public void Execute(Request request)
        {
            if (request == null)
                throw new Exception("Request is null");

            if (string.IsNullOrWhiteSpace(request.Title))
                throw new Exception("Title is null");

            IItem todo = _entitiesFactory.NewTodo();
            todo.Rename(request.Title);
            _itemGateway.Add(todo);

            Response response = new Response(todo.Id);
            _responseHandler.Handle(response);
        }
    }
}