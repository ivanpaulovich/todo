namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System;
    using TodoList.Core.Entities;

    public sealed class TodoItemGateway : ITodoItemGateway
    {
        private readonly InMemoryContext _context;

        public TodoItemGateway(InMemoryContext context)
        {
            _context = context;
        }

        public void Add(ITodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
        }

        public void Delete(ITodoItem todoItem)
        {
            ITodoItem todoItemOld = _context.TodoItems
                .Where(e => e.Id == todoItem.Id)
                .SingleOrDefault();

            _context.TodoItems.Remove(todoItemOld);
        }

        public ITodoItem Get(Guid todoItemId)
        {
            ITodoItem todoItem = _context.TodoItems
                .Where(e => e.Id == todoItemId)
                .SingleOrDefault();

            return todoItem;
        }

        public IList<ITodoItem> List()
        {
            return _context.TodoItems.ToList();
        }

        public void Update(ITodoItem todoItem)
        {
            ITodoItem todoItemOld = _context.TodoItems
                .Where(e => e.Id == todoItem.Id)
                .SingleOrDefault();

            todoItemOld = todoItem;
        }
    }
}