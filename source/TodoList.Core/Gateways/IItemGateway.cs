namespace TodoList.Core.Gateways
{
    using System.Collections.Generic;
    using System;
    using TodoList.Core.Entities;

    public interface IItemGateway
    {
        void Add(IItem item);
        void Delete(string itemId);
        void Update(IItem item);
        IItem Get(string itemId);
        IList<IItem> List();
    }
}