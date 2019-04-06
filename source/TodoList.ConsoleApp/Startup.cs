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
            var entitiesFactory = new Infrastructure.EntityFrameworkGateway.EntitiesFactory();
            ConfigureUseCases(gateway, entitiesFactory);
        }

        internal void ConfigureInMemoryServices()
        {
            var context = new InMemoryContext();
            var gateway = new InMemoryItemGateway(context);
            var entitiesFactory = new Infrastructure.InMemoryGateway.EntitiesFactory();
            ConfigureUseCases(gateway, entitiesFactory);
        }

        internal void ConfigureFileSystemServices()
        {
            var gateway = new FileSystemItemGateway();
            var entitiesFactory = new Infrastructure.FileSystemGateway.EntitiesFactory();
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

                if (command is HelpCommand)
                    DisplayInstructions();

                if (command is InteractiveCommand)
                    interactive = true;

                if (interactive)
                {
                    line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;
                }

            } while (interactive);
        }

        private void DisplayInstructions()
        {
            Console.WriteLine("The usage");
            Console.WriteLine("\ttodo [title]");
            Console.WriteLine("\tren [id] [title]");
            Console.WriteLine("\tdo [id]");
            Console.WriteLine("\tundo [id]");
            Console.WriteLine("\tls");
            Console.WriteLine("\trm [id]");
            Console.WriteLine("\texit");
        }
    }
}