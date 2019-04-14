namespace TodoList.ConsoleApp.Commands
{
    using System;
    using TodoList.ConsoleApp.Controllers;

    public sealed class GistTokenCommand : ICommand
    {
        public string GistToken { get; private set; }

        private readonly string[] Tokens = { "-gt", "/gt", "gt", "gist-token", "--gist-token"};

        public void Execute(TodoItemsController controller)
        {
            controller.Execute(this);
        }

        public bool Match(string[] args)
        {
            if (args.Length != 2)
                return false;

            bool match = CommandArgsParser.Match(args, Tokens);

            if (!match)
                return false;

            GistToken = args[1];

            return true;
        }
    }
}