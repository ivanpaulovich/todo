namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TodoList.Core.UseCases;
    using TodoList.Core.UseCases.AddTodoItem;
    using TodoList.Core.UseCases.ListTodoItems;

    public sealed class OutputHandler :
        IOutputHandler<Core.UseCases.AddTodoItem.Output>,
        IOutputHandler<Core.UseCases.ListTodoItems.Output>
    {
        public Collection<Core.UseCases.AddTodoItem.Output> AddTodoItems { get; }
        public Collection<Core.UseCases.ListTodoItems.Output> ListTodoItems { get; }

        public OutputHandler()
        {
            AddTodoItems = new Collection<Core.UseCases.AddTodoItem.Output>();
            ListTodoItems = new Collection<Core.UseCases.ListTodoItems.Output>();
        }

        public void Handle(Core.UseCases.AddTodoItem.Output output)
        {
            AddTodoItems.Add(output);
        }

        public void Handle(UseCases.ListTodoItems.Output output)
        {
            ListTodoItems.Add(output);
        }
    }
}