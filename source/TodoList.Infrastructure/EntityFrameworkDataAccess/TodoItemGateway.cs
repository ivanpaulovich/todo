using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Core.Entities;
using TodoList.Core.Gateways;

namespace TodoList.Infrastructure.EntityFrameworkDataAccess
{
    public sealed class TodoItemGateway : ITodoItemGateway
    {
        private TodoListContext _todoListContext;

        public TodoItemGateway(TodoListContext todoListContext)
        {
            _todoListContext = todoListContext;
        }

        public void Add(ITodoItem todoItem)
        {
            _todoListContext.TodoItems.Add((TodoItem) todoItem);
            _todoListContext.SaveChanges();
        }

        public void Delete(Guid todoItemId)
        {
            ITodoItem todoItem = Get(todoItemId);

            if (todoItem != null)
            {
                _todoListContext.TodoItems.Remove((TodoItem) todoItem);
                _todoListContext.SaveChanges();
            }
        }

        public ITodoItem Get(Guid todoItemId)
        {
            TodoItem item = _todoListContext.TodoItems.Single(e => e.Id == todoItemId);
            return item;
        }

        public IList<ITodoItem> List()
        {
            var items = _todoListContext.TodoItems;
            return items.Cast<ITodoItem>().ToList();
        }

        public void Update(ITodoItem todoItem)
        {
            _todoListContext.TodoItems.Update((TodoItem) todoItem);
            _todoListContext.SaveChanges();
        }
    }
}