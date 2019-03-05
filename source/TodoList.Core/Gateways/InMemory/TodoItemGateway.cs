namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System;
    using TodoList.Core.Entities;

    public sealed class TodoItemGateway : ITodoItemGateway
    {
        private readonly DBContext _context;

        public TodoItemGateway(DBContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
        }

        public void Delete(TodoItem todoItem)
        {
            TodoItem todoItemOld = _context.TodoItems
                .Where(e => e.Id == todoItem.Id)
                .SingleOrDefault();

            _context.TodoItems.Remove(todoItemOld);
        }

        public TodoItem Get(Guid todoItemId)
        {
            TodoItem todoItem = _context.TodoItems
                .Where(e => e.Id == todoItemId)
                .SingleOrDefault();

            return todoItem;
        }

        public IList<TodoItem> List()
        {
            return _context.TodoItems;
        }

        public void Update(TodoItem todoItem)
        {
            TodoItem todoItemOld = _context.TodoItems
                .Where(e => e.Id == todoItem.Id)
                .SingleOrDefault();

            todoItemOld = todoItem;
        }
    }
}