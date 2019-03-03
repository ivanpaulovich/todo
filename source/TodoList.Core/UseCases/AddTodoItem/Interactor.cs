namespace TodoList.Core.UseCases.AddTodoItem {
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class Interactor : IUseCase<Input> {
        private IOutputHandler _outputHandler;
        private ITodoItemGateway _todoItemGateway;

        public Interactor (
            IOutputHandler outputHandler,
            ITodoItemGateway todoItemGateway) {
            _outputHandler = outputHandler;
            _todoItemGateway = todoItemGateway;
        }

        public void Execute (Input input) {
            if (input == null)
                throw new Exception ("Input is null");

            if (string.IsNullOrWhiteSpace (input.Title))
                throw new Exception ("Title is null");

            TodoItem todoItem = new TodoItem(input.Title);
            _todoItemGateway.Add(todoItem);

            Output output = new Output (todoItem.Id);
            _outputHandler.Handle (output);
        }
    }
}