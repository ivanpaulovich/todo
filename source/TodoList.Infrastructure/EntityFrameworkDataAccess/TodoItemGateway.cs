using System;
using System.Collections.Generic;
using TodoList.Core.Entities;
using TodoList.Core.Gateways;
using System.Linq;

namespace TodoList.Infrastructure.EntityFrameworkDataAccess
{
    public sealed class TodoItemGateway : ITodoItemGateway
    {
        private TodoListContext _todoListContext;

        public TodoItemGateway(TodoListContext todoListContext)
        {
            _todoListContext = todoListContext;
        }

        public void Add(TodoItem todoItem)
        {
            _todoListContext.TodoItems.Add (todoItem);
            _todoListContext.SaveChanges ();
        }

        public void Delete(TodoItem todoItem)
        {
            _todoListContext.TodoItems.Remove (todoItem);
            _todoListContext.SaveChanges ();
        }

        public TodoItem Get(Guid todoItemId)
        {
            TodoItem item = _todoListContext.TodoItems.Single (e => e.Id == todoItemId);
            return item;
        }

        public IList<TodoItem> List()
        {
            var items = _todoListContext.TodoItems;
            return items.ToList();
        }

        public void Update(TodoItem todoItem)
        {
            _todoListContext.TodoItems.Update (todoItem);
            _todoListContext.SaveChanges ();
        }
    }
}