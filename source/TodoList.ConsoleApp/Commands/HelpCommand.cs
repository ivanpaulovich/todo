namespace TodoList.ConsoleApp.Commands
{
    using System;
    using TodoList.ConsoleApp.Controllers;

    public sealed class HelpCommand : ICommand
    {
        private readonly string[] Tokens = { "-h", "/h", "h", "help", "--help", "" };

        public void Execute(TodoItemsController controller)
        {
            controller.Help();
        }

        public bool Match(string[] args)
        {
            if (args.Length != 1)
                return false;

            bool match = CommandArgsParser.Match(args, Tokens);
            return match;
        }
    }
}