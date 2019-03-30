namespace TodoList.Core.Boundaries.List
{
    using System.Collections.Generic;
    using System;

    public sealed class ResponseBuilder
    {
        private IList<Item> _items;

        public ResponseBuilder()
        {
            _items = new List<Item>();
        }

        public ResponseBuilder WithDone(Guid itemId, string title)
        {
            _items.Add(new Item(itemId, title, true));
            return this;
        }

        public ResponseBuilder WithUndone(Guid itemId, string title)
        {
            _items.Add(new Item(itemId, title, false));
            return this;
        }

        public Response Build()
        {
            var response = new Response(_items);
            return response;
        }
    }
}