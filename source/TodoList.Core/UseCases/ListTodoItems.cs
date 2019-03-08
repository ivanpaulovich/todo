namespace TodoList.Core.UseCases
{
    using System.Collections.Generic;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Boundaries.ListTodoItems;
    using TodoList.Core.Gateways;

    public sealed class ListTodoItems : IUseCase
    {
        private IResponseHandler<Response> _outputHandler;
        private ITodoItemGateway _todoItemGateway;

        public ListTodoItems(
            IResponseHandler<Response> outputHandler,
            ITodoItemGateway todoItemGateway)
        {
            _outputHandler = outputHandler;
            _todoItemGateway = todoItemGateway;
        }

        public void Execute()
        {
            var todoItems = _todoItemGateway.List();
            Response output = BuildOutput(todoItems);
            _outputHandler.Handle(output);
        }

        private Response BuildOutput(IList<Entities.TodoItem> todoItems)
        {
            ResponseBuilder builder = new ResponseBuilder();
            foreach (var item in todoItems)
                builder.WithItem(item.Id, item.Title);

            return builder.Build();
        }
    }
}