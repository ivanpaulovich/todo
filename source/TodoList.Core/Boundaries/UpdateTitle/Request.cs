namespace TodoList.Core.Boundaries.UpdateTitle
{
    using System;

    public sealed class Request : IRequest
    {
        public Request(Guid todoItemId, string title)
        {
            TodoItemId = todoItemId;
            Title = title;
        }

        public Guid TodoItemId { get; }
        public string Title { get; }
    }
}