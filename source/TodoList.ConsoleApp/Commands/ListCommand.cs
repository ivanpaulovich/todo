namespace TodoList.ConsoleApp.Commands
{
    using System;
    using TodoList.ConsoleApp.Controllers;

    public sealed class ListCommand : ICommand
    {
        private readonly string[] Tokens = { "-l", "/l", "l", "-ls", "/ls", "ls", "list", "--list" };

        public void Execute(TodoItemsController controller)
        {
            controller.List();
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