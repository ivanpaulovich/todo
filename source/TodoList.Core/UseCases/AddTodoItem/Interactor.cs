namespace TodoList.Core.UseCases.AddTodoItem
{
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class Interactor : IUseCase<AddTodoItemRequest>
    {
        private IOutputHandler<AddTodoItemResponse> _outputHandler;
        private ITodoItemGateway _todoItemGateway;
        private IEntitiesFactory _entitiesFactory;

        public Interactor(
            IOutputHandler<AddTodoItemResponse> outputHandler,
            ITodoItemGateway todoItemGateway,
            IEntitiesFactory entitiesFactory)
        {
            _outputHandler = outputHandler;
            _todoItemGateway = todoItemGateway;
            _entitiesFactory = entitiesFactory;
        }

        public void Execute(AddTodoItemRequest input)
        {
            if (input == null)
                throw new Exception("Input is null");

            if (string.IsNullOrWhiteSpace(input.Title))
                throw new Exception("Title is null");

            TodoItem todoItem = _entitiesFactory.NewTodoItem(input.Title);
            _todoItemGateway.Add(todoItem);

            AddTodoItemResponse output = new AddTodoItemResponse(todoItem.Id);
            _outputHandler.Handle(output);
        }
    }
}