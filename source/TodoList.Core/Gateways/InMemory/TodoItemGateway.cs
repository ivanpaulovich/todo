namespace TodoList.Core.Gateways.InMemory {
    using System.Collections.ObjectModel;
    using TodoList.Core.Entities;
    using System.Linq;
    using System;

    public sealed class TodoItemGateway : ITodoItemGateway {
        private readonly Context _context;

        public TodoItemGateway (Context context) {
            _context = context;
        }

        public void Add (TodoItem todoItem) {
            _context.TodoItems.Add (todoItem);
        }

        public void Delete (TodoItem todoItem) {
            TodoItem todoItemOld = _context.TodoItems
                .Where (e => e.Id == todoItem.Id)
                .SingleOrDefault ();

            _context.TodoItems.Remove (todoItemOld);
        }

        public TodoItem Get (Guid todoItemId) {
            TodoItem todoItem = _context.TodoItems
                .Where (e => e.Id == todoItemId)
                .SingleOrDefault ();

            return todoItem;
        }

        public void Update (TodoItem todoItem) {
            TodoItem todoItemOld = _context.TodoItems
                .Where (e => e.Id == todoItem.Id)
                .SingleOrDefault ();

            todoItemOld = todoItem;
        }
    }
}