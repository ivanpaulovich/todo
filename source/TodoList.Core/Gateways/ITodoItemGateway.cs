namespace TodoList.Core.Gateways
{
    using System.Collections.Generic;
    using System;
    using TodoList.Core.Entities;

    public interface ITodoItemGateway
    {
        void Add(TodoItem todoItem);
        void Delete(TodoItem todoItem);
        void Update(TodoItem todoItem);
        TodoItem Get(Guid todoItemId);
        IList<TodoItem> List();
    }
}