namespace TodoList.Core.UseCases.ListTodoItems
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

        public ListTodoItemsResponse Build()
        {
            var output = new ListTodoItemsResponse(_todoItems);
            return output;
        }
    }
}