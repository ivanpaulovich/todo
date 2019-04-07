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