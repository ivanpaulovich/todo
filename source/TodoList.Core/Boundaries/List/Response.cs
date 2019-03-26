namespace TodoList.Core.Boundaries.List
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public sealed class Response
    {
        public IReadOnlyCollection<Item> Items { get; }

        public Response(IList<Item> items)
        {
            Items = new ReadOnlyCollection<Item>(items);
        }
    }
}