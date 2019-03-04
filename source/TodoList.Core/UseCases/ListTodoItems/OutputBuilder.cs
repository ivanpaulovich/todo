namespace TodoList.Core.UseCases.ListTodoItems {
    using System.Collections.Generic;
    using System;

    public sealed class OutputBuilder {
        private IList<TodoItem> _todoItems;

        public OutputBuilder () {
            _todoItems = new List<TodoItem> ();
        }

        public OutputBuilder WithItem (Guid todoItemId, string title) {
            _todoItems.Add (new TodoItem (todoItemId, title));
            return this;
        }

        public Output Build () {
            var output = new Output (_todoItems);
            return output;
        }
    }
}