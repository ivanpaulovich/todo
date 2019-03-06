namespace TodoList.Core.UseCases.ListTodoItems
{
    using System.Collections.Generic;
    using TodoList.Core.Gateways;

    public sealed class Interactor : IUseCase
    {
        private IOutputHandler<ListTodoItemsResponse> _outputHandler;
        private ITodoItemGateway _todoItemGateway;

        public Interactor(
            IOutputHandler<ListTodoItemsResponse> outputHandler,
            ITodoItemGateway todoItemGateway)
        {
            _outputHandler = outputHandler;
            _todoItemGateway = todoItemGateway;
        }

        public void Execute()
        {
            var todoItems = _todoItemGateway.List();
            ListTodoItemsResponse output = CreateOutput(todoItems);
            _outputHandler.Handle(output);
        }

        private ListTodoItemsResponse CreateOutput(IList<Entities.TodoItem> todoItems)
        {
            ResponseBuilder output = new ResponseBuilder();
            foreach (var item in todoItems)
                output.WithItem(item.Id, item.Title);

            return output.Build();
        }
    }
}