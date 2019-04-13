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

            bool match = false;

            foreach (var token in Tokens)
                if (string.Compare(args[0].Trim(), token, StringComparison.CurrentCultureIgnoreCase) == 0)
                    match = true;

            if (!match)
                return false;

            return true;
        }
    }
}