namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.ObjectModel;
    using TodoList.Core.Entities;
    using System.Linq;
    using System;

    public sealed class InMemoryContext
    {
        public Collection<ITodoItem> TodoItems { get; set; }

        public InMemoryContext()
        {
            TodoItems = new Collection<ITodoItem>();
            TodoItems.Add(TodoItem.Restore(new Guid("3b35f11e-7080-45e2-a152-afff5a325508"), "Create Repository"));
            TodoItems.Add(TodoItem.Restore(new Guid("4b2f8170-c618-4cd6-91b9-25e3b2bfa53e"), "Create solution"));
            TodoItems.Add(TodoItem.Restore(new Guid("360644f3-abb5-410b-939d-78a6d07bd075"), "Add projects"));
            TodoItems.Add(TodoItem.Restore(new Guid("f1f0adf8-255f-45ef-9528-d6c2c326240b"), "Commit code"));
            TodoItems.Add(TodoItem.Restore(new Guid("72af359b-48d7-41cd-978b-38c82e1206d4"), "Push"));
        }
    }
}