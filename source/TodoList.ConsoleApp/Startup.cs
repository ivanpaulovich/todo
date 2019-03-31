namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.ConsoleApp.Commands;
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

        internal void Run(string[] args)
        {
            string line = string.Join(' ', args);
            bool interactive = false;

            do
            {
                CommandParser commandParser = new CommandParser();
                ICommand command = commandParser.ParseCommand(line);

                if (command is TodoCommand todoCommand)
                {
                    var request = new TodoList.Core.Boundaries.Todo.Request(todoCommand.Title);
                    _todoUseCase.Execute(request);
                }

                if (command is RemoveCommand removeCommand)
                    _removeUseCase.Execute(removeCommand.Id);

                if (command is ListCommand listCommand)
                    _listUseCase.Execute();

                if (command is RenameCommand renameCommand)
                {
                    var request = new TodoList.Core.Boundaries.Rename.Request(renameCommand.Id, renameCommand.NewTitle);
                    _renameUseCase.Execute(request);
                }

                if (command is DoCommand doCommand)
                    _doUseCase.Execute(doCommand.Id);

                if (command is UndoCommand undoCommand)
                    _undoUseCase.Execute(undoCommand.Id);

                if (command is HelpCommand helpCommand)
                    _presenter.DisplayInstructions();

                if (command is InteractiveCommand interactiveCommand)
                    interactive = true;

                if (interactive)
                {
                    line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;
                }

            } while (true);
        }
    }
}