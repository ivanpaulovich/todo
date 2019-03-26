namespace TodoList.Core.Boundaries.Todo
{
    using System;

    public sealed class Response
    {
        public Guid ItemId { get; }

        public Response(Guid itemId)
        {
            ItemId = itemId;
        }
    }
}