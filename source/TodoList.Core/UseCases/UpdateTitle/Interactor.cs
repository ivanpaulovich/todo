namespace TodoList.Core.UseCases.UpdateTitle
{
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class Interactor : IUseCase<Input>
    {
        private ITodoItemGateway _todoItemGateway;

        public Interactor(
            ITodoItemGateway todoItemGateway)
        {
            _todoItemGateway = todoItemGateway;
        }

        public void Execute(Input input)
        {
            if (input == null)
                throw new Exception("Input is null");

            if (string.IsNullOrWhiteSpace(input.Title))
                throw new Exception("Title is null");

            TodoItem todoItem = _todoItemGateway.Get(input.TodoItemId);
            todoItem.UpdateTitle(input.Title);
            _todoItemGateway.Update(todoItem);
        }
    }
}