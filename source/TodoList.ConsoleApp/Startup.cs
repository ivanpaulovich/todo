namespace TodoList.ConsoleApp
{
    using System.Collections.Generic;
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using TodoList.ConsoleApp.Commands;
    using TodoList.ConsoleApp.Controllers;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using TodoList.Infrastructure.EntityFrameworkGateway;
    using TodoList.Infrastructure.FileSystemGateway;

    internal sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private TodoItemsController controller;

        internal void ConfigureSqlServerServices()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            SqlContext context = new SqlContext(optionsBuilder.Options);
            IItemGateway gateway = new SqlItemGateway(context);
            EntitiesFactory entitiesFactory = new EntitiesFactory();
            ConfigureUseCases(gateway, entitiesFactory);
        }

        internal void ConfigureInMemoryServices()
        {
            InMemoryContext context = new InMemoryContext();
            IItemGateway gateway = new InMemoryItemGateway(context);
            EntitiesFactory entitiesFactory = new EntitiesFactory();
            ConfigureUseCases(gateway, entitiesFactory);
        }

        internal void ConfigureFileSystemServices()
        {
            IItemGateway gateway = new FileSystemItemGateway();
            EntitiesFactory entitiesFactory = new EntitiesFactory();
            ConfigureUseCases(gateway, entitiesFactory);
        }

        internal void ConfigureUseCases(
            IItemGateway gateway,
            IEntitiesFactory entitiesFactory)
        {
            Presenter presenter = new Presenter();

            var renameUseCase = new Core.UseCases.Rename(gateway);
            var listUseCase = new Core.UseCases.List(presenter, gateway);
            var removeUseCase = new Core.UseCases.Remove(gateway);
            var todoUseCase = new Core.UseCases.Todo(presenter, gateway, entitiesFactory);
            var doUseCase = new Core.UseCases.Do(gateway);
            var undoUseCase = new Core.UseCases.Undo(gateway);

            controller = new TodoItemsController(
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
                    controller.Execute(todoCommand);

                if (command is RemoveCommand removeCommand)
                    controller.Execute(removeCommand);

                if (command is ListCommand listCommand)
                    controller.Execute(listCommand);

                if (command is RenameCommand renameCommand)
                    controller.Execute(renameCommand);

                if (command is DoCommand doCommand)
                    controller.Execute(doCommand);

                if (command is UndoCommand undoCommand)
                    controller.Execute(undoCommand);

                if (command is HelpCommand helpCommand)
                    controller.Execute(helpCommand);

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