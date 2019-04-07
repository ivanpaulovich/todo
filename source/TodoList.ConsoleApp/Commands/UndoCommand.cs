namespace TodoList.ConsoleApp.Commands
{
    using System;
    using TodoList.ConsoleApp.Controllers;

    public sealed class UndoCommand : ICommand
    {
        public string Id { get; private set; }

        private readonly string[] Tokens = { "-u", "/u", "undo", "--undo" };

        public void Execute(TodoItemsController controller)
        {
            controller.Execute(this);
        }

        public bool Match(string[] args)
        {
            if (args.Length != 2)
                return false;

            bool match = false;

            foreach (var token in Tokens)
                if (string.Compare(args[0].Trim(), token, StringComparison.CurrentCultureIgnoreCase) == 0)
                    match = true;

            if (!match)
                return false;

            Id = args[1];

            return true;
        }
    }
}