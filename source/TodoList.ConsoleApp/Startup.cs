namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using TodoList.Infrastructure.FileSystemGateway;

    internal sealed class Startup
    {
        private Presenter _presenter;
        private IUseCase<Core.Boundaries.Todo.Request> _todoUseCase;
        private TodoList.Core.Boundaries.Remove.IUseCase _removeUseCase;
        private TodoList.Core.Boundaries.List.IUseCase _listUseCase;
        private IUseCase<Core.Boundaries.Rename.Request> _renameUseCase;
        private TodoList.Core.Boundaries.Do.IUseCase _doUseCase;
        private TodoList.Core.Boundaries.Undo.IUseCase _undoUseCase;

        internal void ConfigureServices()
        {
            IItemGateway gateway = new FileSystemItemGateway();
            EntitiesFactory entitiesFactory = new EntitiesFactory();
            _presenter = new Presenter();
            _renameUseCase = new Core.UseCases.Rename(gateway);
            _listUseCase = new Core.UseCases.List(_presenter, gateway);
            _removeUseCase = new Core.UseCases.Remove(gateway);
            _todoUseCase = new Core.UseCases.Todo(_presenter, gateway, entitiesFactory);
            _doUseCase = new Core.UseCases.Do(gateway);
            _undoUseCase = new Core.UseCases.Undo(gateway);
        }

        private void Rename(string id, string title)
        {
            var input = new TodoList.Core.Boundaries.Rename.Request(id, title);
            _renameUseCase.Execute(input);
        }

        private void List()
        {
            _listUseCase.Execute();
        }

        private void Remove(string id)
        {
            _removeUseCase.Execute(id);
        }

        private void Todo(string title)
        {
            var input = new TodoList.Core.Boundaries.Todo.Request(title);
            _todoUseCase.Execute(input);
        }

        private void Undo(string id)
        {
            _undoUseCase.Execute(id);
        }

        private void Do(string id)
        {
            _doUseCase.Execute(id);
        }

        internal void Run(CommandType commandType, string id, string title)
        {
            if (commandType == CommandType.Todo)
                Todo(title);

            if (commandType == CommandType.Remove)
                Remove(id);

            if (commandType == CommandType.List)
                List();

            if (commandType == CommandType.Rename)
                Rename(id, title);

            if (commandType == CommandType.Do)
                Do(id);

            if (commandType == CommandType.Undo)
                Undo(id);
        }
    }
}