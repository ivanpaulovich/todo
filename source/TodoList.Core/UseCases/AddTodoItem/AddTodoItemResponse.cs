namespace TodoList.Core.UseCases.AddTodoItem
{
    using System;

    public sealed class AddTodoItemResponse
    {
        public Guid Id { get; }

        public AddTodoItemResponse(Guid id)
        {
            Id = id;
        }
    }
}