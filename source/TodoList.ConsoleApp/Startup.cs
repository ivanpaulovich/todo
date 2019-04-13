namespace TodoList.ConsoleApp
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using TodoList.ConsoleApp.Commands;
    using TodoList.ConsoleApp.Controllers;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Infrastructure.EntityFrameworkGateway;
    using TodoList.Infrastructure.FileSystemGateway;
    using TodoList.Infrastructure.GistGateway;
    using TodoList.Infrastructure.InMemoryGateway;

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
            var context = new SqlContext(optionsBuilder.Options);
            var gateway = new SqlItemGateway(context);
            var entitiesFactory = new DefaultEntitiesFactory();
            ConfigureUseCases(gateway, entitiesFactory);
        }

        internal void ConfigureInMemoryServices()
        {
            var context = new InMemoryContext();
            var gateway = new InMemoryItemGateway(context);
            var entitiesFactory = new DefaultEntitiesFactory();
            ConfigureUseCases(gateway, entitiesFactory);
        }

        internal void ConfigureFileSystemServices()
        {
            var gateway = new FileSystemItemGateway();
            var entitiesFactory = new DefaultEntitiesFactory();
            ConfigureUseCases(gateway, entitiesFactory);
        }

        internal void ConfigureGistServices()
        {
            var gateway = new GistItemGateway();
            var entitiesFactory = new DefaultEntitiesFactory();
            ConfigureUseCases(gateway, entitiesFactory);
        }

        internal void ConfigureUseCases(
            IItemGateway gateway,
            IEntitiesFactory entitiesFactory)
        {
            ListPresenter listPresenter = new ListPresenter();
            TodoPresenter todoPresenter = new TodoPresenter();

            var renameUseCase = new Rename(gateway);
            var listUseCase = new List(listPresenter, gateway);
            var removeUseCase = new Remove(gateway);
            var todoUseCase = new Todo(todoPresenter, gateway, entitiesFactory);
            var doUseCase = new Do(gateway);
            var undoUseCase = new Undo(gateway);

            controller = new TodoItemsController(
                todoUseCase,
                removeUseCase,
                listUseCase,
                renameUseCase,
                doUseCase,
                undoUseCase
            );
        }

        internal void Run(string[] args)
        {
            do
            {
                CommandParser commandParser = new CommandParser();
                ICommand command = commandParser.ParseCommand(args);
                command.Execute(controller);

                if (controller.IsInteractive)
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;
                    else
                        args = CommandArgsParser.TokenizeCommandLineToStringArray(line);
                }

            } while (controller.IsInteractive);
        }
    }
}