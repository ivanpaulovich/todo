namespace TodoList.ConsoleApp.Commands
{
    using System;

    public sealed class CommandParser
    {
        private ICommand[] commands;

        public CommandParser()
        {
            commands = new ICommand[]
            {
                new DevelopmentModeCommand(),
                new InteractiveCommand(),
                new DoCommand(),
                new HelpCommand(),
                new ListCommand(),
                new RemoveCommand(),
                new RenameCommand(),
                new TodoCommand(),
                new UndoCommand(),
                new GistTokenCommand(),
                new GistIdCommand()
            };
        }

        public ICommand ParseCommand(string[] args)
        {
            foreach (var command in commands)
                if (command.Match(args))
                    return command;

            return new HelpCommand();
        }
    }
}