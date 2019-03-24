namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;

    public sealed class Startup
    {
        private IUseCase<Core.Boundaries.AddTodoItem.Request> _addTodoItem;
        private TodoList.Core.Boundaries.RemoveTodoItem.IUseCase _removeTodoItem;
        private TodoList.Core.Boundaries.ListTodoItems.IUseCase _listTodoItems;
        private IUseCase<Core.Boundaries.UpdateTitle.Request> _updateTitle;

        public Startup(
            IUseCase<Core.Boundaries.AddTodoItem.Request> addTodoItem,
            TodoList.Core.Boundaries.RemoveTodoItem.IUseCase removeTodoItem,
            TodoList.Core.Boundaries.ListTodoItems.IUseCase listTodoItems,
            IUseCase<TodoList.Core.Boundaries.UpdateTitle.Request> updateTitle)
        {
            _addTodoItem = addTodoItem;
            _removeTodoItem = removeTodoItem;
            _listTodoItems = listTodoItems;
            _updateTitle = updateTitle;
        }

        public void UpdateTodoItem(string[] args)
        {
            if (args.Length != 3)
                return;

            Guid id;

            if (!Guid.TryParse(args[1], out id))
                return;

            var input = new TodoList.Core.Boundaries.UpdateTitle.Request(id, args[2]);
            _updateTitle.Execute(input);
        }

        public void ListTodoItem(string[] args)
        {
            if (args.Length != 1)
                return;

            _listTodoItems.Execute();
        }

        public void RemoveTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            _removeTodoItem.Execute(new Guid(args[1]));
        }

        public void AddTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            var input = new TodoList.Core.Boundaries.AddTodoItem.Request(args[1]);
            _addTodoItem.Execute(input);
        }
    }
}