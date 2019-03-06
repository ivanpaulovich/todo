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
        private ITodoItemGateway _gateway;
        private IEntitiesFactory _entitiesFactory;
        private Presenter _presenter;

        public Startup(
            ITodoItemGateway gateway,
            IEntitiesFactory entitiesFactory,
            Presenter presenter)
        {
            _gateway = gateway;
            _entitiesFactory = entitiesFactory;
            _presenter = presenter;
        }

        public void UpdateTodoItem(string[] args)
        {
            if (args.Length != 3)
                return;

            Guid id;

            if (!Guid.TryParse(args[1], out id))
                return;

            var updateTodoItem = new Core.UseCases.UpdateTitle.Interactor(
                _gateway
            );

            var input = new Core.UseCases.UpdateTitle.Input(id, args[2]);
            updateTodoItem.Execute(input);
        }

        public void ListTodoItem(string[] args)
        {
            if (args.Length != 1)
                return;

            var list = new Core.UseCases.ListTodoItems.Interactor(
                _presenter,
                _gateway
            );

            list.Execute(new Core.UseCases.ListTodoItems.Input());
        }

        public void FinishTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            IUseCase<Guid> finish = new Core.UseCases.FinishTodoItem.Interactor(
                _gateway
            );

            finish.Execute(new Guid(args[1]));
        }

        public void AddTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            IUseCase<Core.UseCases.AddTodoItem.Input> addTodoItem = new Core.UseCases.AddTodoItem.Interactor(
                _presenter,
                _gateway,
                _entitiesFactory
            );

            var input = new Core.UseCases.AddTodoItem.Input(args[1]);
            addTodoItem.Execute(input);
        }
    }
}