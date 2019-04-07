namespace TodoList.ConsoleApp.Commands
{
    using System;
    using TodoList.ConsoleApp.Controllers;

    public sealed class RenameCommand : ICommand
    {
        public string Id { get; private set; }
        public string NewTitle { get; private set; }

        private readonly string[] Tokens = { "-ren", "/ren", "ren", "-r", "/r", "r", "rename", "--rename" };


        public void Execute(TodoItemsController controller)
        {
            controller.Execute(this);
        }

        public bool Match(string[] args)
        {
            if (args.Length != 3)
                return false;

            bool match = false;

            foreach (var token in Tokens)
                if (string.Compare(args[0].Trim(), token, StringComparison.CurrentCultureIgnoreCase) == 0)
                    match = true;

            if (!match)
                return false;

            Id = args[1];
            NewTitle = args[2];

            return true;
        }
    }
}