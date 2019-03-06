namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using TodoList.Core.Entities;

    public sealed class Startup 
    {
        private IUseCase<Core.UseCases.AddTodoItem.Input> _addTodoItem;
        private IUseCase<Guid> _finishTodoItem;
        private IUseCase<Core.UseCases.ListTodoItems.Input> _listTodoItems;
        private IUseCase<Core.UseCases.UpdateTitle.Input> _updateTitle;

        public Startup(
            IUseCase<Core.UseCases.AddTodoItem.Input> addTodoItem,
            IUseCase<Guid> finishTodoItem,
            IUseCase<Core.UseCases.ListTodoItems.Input> listTodoItems,
            IUseCase<Core.UseCases.UpdateTitle.Input> updateTitle)
        {
            _addTodoItem = addTodoItem;
            _finishTodoItem = finishTodoItem;
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

            var input = new Core.UseCases.UpdateTitle.Input(id, args[2]);
            _updateTitle.Execute(input);
        }

        public void ListTodoItem(string[] args)
        {
            if (args.Length != 1)
                return;

            _listTodoItems.Execute(new Core.UseCases.ListTodoItems.Input());
        }

        public void FinishTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            _finishTodoItem.Execute(new Guid(args[1]));
        }

        public void AddTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            var input = new Core.UseCases.AddTodoItem.Input(args[1]);
            _addTodoItem.Execute(input);
        }
    }
}