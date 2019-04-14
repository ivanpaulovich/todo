namespace TodoList.ConsoleApp.Commands
{
    using System;
    using TodoList.ConsoleApp.Controllers;

    public sealed class InteractiveCommand : ICommand
    {
        private readonly string[] Tokens = { "-i", "/i", "i", "interactive", "--interactive" };

        public void Execute(TodoItemsController controller)
        {
            controller.Interactive();
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