namespace TodoList.Core.Boundaries.AddTodoItem
{
    using System;

    public sealed class Response
    {
        public Guid Id { get; }

        public Response(Guid id)
        {
            Id = id;
        }
    }
}