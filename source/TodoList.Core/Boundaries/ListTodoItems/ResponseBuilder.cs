namespace TodoList.Core.Boundaries.ListTodoItems
{
    using System.Collections.Generic;
    using System;

    public sealed class ResponseBuilder
    {
        private IList<TodoItem> _todoItems;

        public ResponseBuilder()
        {
            _todoItems = new List<TodoItem>();
        }

        public ResponseBuilder WithItem(Guid todoItemId, string title)
        {
            _todoItems.Add(new TodoItem(todoItemId, title));
            return this;
        }

        public Response Build()
        {
            var output = new Response(_todoItems);
            return output;
        }
    }
}