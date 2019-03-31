namespace TodoList.Infrastructure.EntityFrameworkGateway
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class SqlItemGateway : IItemGateway
    {
        private SqlContext _todoContext;

        public SqlItemGateway(SqlContext todoContext)
        {
            _todoContext = todoContext;
        }

        public void Add(IItem todoItem)
        {
            _todoContext.Items.Add((Item) todoItem);
            _todoContext.SaveChanges();
        }

        public void Delete(string itemId)
        {
            Item item = (Item) Get(itemId);

            if (item != null)
            {
                _todoContext.Items.Remove(item);
                _todoContext.SaveChanges();
            }
        }

        public IItem Get(string itemId)
        {
            Item item = _todoContext
                .Items
                .SingleOrDefault(e => e.Id.ToString().StartsWith(itemId));

            return item;
        }

        public IList<IItem> List()
        {
            var items = _todoContext.Items;
            return items.Cast<IItem>().ToList();
        }

        public void Update(IItem item)
        {
            _todoContext.Items.Update((Item) item);
            _todoContext.SaveChanges();
        }
    }
}