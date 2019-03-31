namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Exceptions;

    public sealed class InMemoryItemGateway : IItemGateway
    {
        private readonly InMemoryContext _context;

        public InMemoryItemGateway(InMemoryContext context)
        {
            _context = context;
        }

        public void Add(IItem item)
        {
            _context.Items.Add(item);
        }

        public void Delete(string itemId)
        {
            IItem existingItem = _context.Items
                .Where(e => e.Id.ToString().StartsWith(itemId))
                .SingleOrDefault();

            _context.Items.Remove(existingItem);
        }

        public IItem Get(string itemId)
        {
            IItem existingItem = _context.Items
                .Where(e => e.Id.ToString().StartsWith(itemId))
                .SingleOrDefault();

            return existingItem;
        }

        public IList<IItem> List()
        {
            return _context.Items.ToList();
        }

        public void Update(IItem item)
        {
            IItem oldItem = _context.Items
                .Where(e => e.Id == item.Id)
                .SingleOrDefault();

            oldItem = item;
        }
    }
}