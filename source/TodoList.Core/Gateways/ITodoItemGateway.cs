namespace TodoList.Core.Gateways
{
    using System.Collections.Generic;
    using System;
    using TodoList.Core.Entities;

    public interface ITodoItemGateway
    {
        void Add(ITodoItem todoItem);
        void Delete(ITodoItem todoItem);
        void Update(ITodoItem todoItem);
        ITodoItem Get(Guid todoItemId);
        IList<ITodoItem> List();
    }
}