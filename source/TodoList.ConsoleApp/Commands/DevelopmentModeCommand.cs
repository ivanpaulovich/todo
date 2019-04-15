namespace TodoList.ConsoleApp.Commands
{
    using System;
    using TodoList.ConsoleApp.Controllers;

    public sealed class DevelopmentModeCommand : ICommand
    {
        private readonly string[] Tokens = { "-dm", "/dm", "dm", "dev-mode", "--dev-mode" };

        public void Execute(TodoItemsController controller)
        {
            controller.DevelopmentMode();
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