namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Exceptions;

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

        public void Delete(Guid todoItemId)
        {
            ITodoItem existingTodoItem = _context.TodoItems
                .Where(e => e.Id == todoItemId)
                .SingleOrDefault();

            if (existingTodoItem == null)
                throw new BusinessException($"Item with id { todoItemId } was not found.");

            _context.TodoItems.Remove(existingTodoItem);
        }

        public ITodoItem Get(Guid todoItemId)
        {
            ITodoItem existingTodoItem = _context.TodoItems
                .Where(e => e.Id == todoItemId)
                .SingleOrDefault();

            if (existingTodoItem == null)
                throw new BusinessException($"Item with id { todoItemId } was not found.");

            return existingTodoItem;
        }

        public IList<ITodoItem> List()
        {
            return _context.TodoItems.ToList();
        }

        public void Update(ITodoItem todoItem)
        {
            ITodoItem oldTodoItem = _context.TodoItems
                .Where(e => e.Id == todoItem.Id)
                .SingleOrDefault();

            if (oldTodoItem == null)
                throw new BusinessException($"Item with id { todoItem.Id } was not found.");

            oldTodoItem = todoItem;
        }
    }
}