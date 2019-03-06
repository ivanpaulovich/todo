namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases.AddTodoItem;
    using TodoList.Core.UseCases.UpdateTitle;
    using TodoList.Core.UseCases;
    using TodoList.Core;

    public sealed class Startup
    {
        private IUseCase<AddTodoItemRequest> _addTodoItem;
        private IUseCase<Guid> _finishTodoItem;
        private IUseCase _listTodoItems;
        private IUseCase<UpdateTitleRequest> _updateTitle;

        public Startup(
            IUseCase<AddTodoItemRequest> addTodoItem,
            IUseCase<Guid> finishTodoItem,
            IUseCase listTodoItems,
            IUseCase<UpdateTitleRequest> updateTitle)
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

            var input = new UpdateTitleRequest(id, args[2]);
            _updateTitle.Execute(input);
        }

        public void ListTodoItem(string[] args)
        {
            if (args.Length != 1)
                return;

            _listTodoItems.Execute();
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

            var input = new AddTodoItemRequest(args[1]);
            _addTodoItem.Execute(input);
        }
    }
}