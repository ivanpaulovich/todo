namespace TodoList.Core.Gateways {
    using System;
    using TodoList.Core.Entities;

    public interface ITodoItemGateway {
        void Add (TodoItem todoItem);
        void Delete (TodoItem todoItem);
        TodoItem Update (TodoItem todoItem);
        TodoItem Get (Guid todoItemId);
    }
}