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
    using TodoList.ConsoleApp.Controllers;
    using System.Collections.Generic;

    internal sealed class Startup
    {
        private TodoItemsController _controller;

        internal void ConfigureServices()
        {
            IItemGateway gateway = new FileSystemItemGateway();
            EntitiesFactory entitiesFactory = new EntitiesFactory();
            Presenter presenter = new Presenter();

            var renameUseCase = new Core.UseCases.Rename(gateway);
            var listUseCase = new Core.UseCases.List(presenter, gateway);
            var removeUseCase = new Core.UseCases.Remove(gateway);
            var todoUseCase = new Core.UseCases.Todo(presenter, gateway, entitiesFactory);
            var doUseCase = new Core.UseCases.Do(gateway);
            var undoUseCase = new Core.UseCases.Undo(gateway);
            
            _controller = new TodoItemsController(
                todoUseCase,
                removeUseCase,
                listUseCase,
                renameUseCase,
                doUseCase,
                undoUseCase,
                presenter
            );
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
                    _controller.Execute(todoCommand);

                if (command is RemoveCommand removeCommand)
                    _controller.Execute(removeCommand);

                if (command is ListCommand listCommand)
                    _controller.Execute(listCommand);

                if (command is RenameCommand renameCommand)
                    _controller.Execute(renameCommand);

                if (command is DoCommand doCommand)
                    _controller.Execute(doCommand);

                if (command is UndoCommand undoCommand)
                    _controller.Execute(undoCommand);

                if (command is HelpCommand helpCommand)
                    _controller.Execute(helpCommand);

                if (command is InteractiveCommand interactiveCommand)
                    interactive = true;

                if (interactive)
                {
                    line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;
                }

            } while (interactive);
        }
    }
}