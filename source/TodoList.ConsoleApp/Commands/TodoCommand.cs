namespace TodoList.ConsoleApp.Commands
{
    using System;
    using TodoList.ConsoleApp.Controllers;

    public sealed class TodoCommand : ICommand
    {
        public string Title { get; private set; }
        
        public void Execute(TodoItemsController controller)
        {
            controller.Execute(this);
        }

        public bool Match(string[] args)
        {
            if (args.Length != 1)
                return false;

            Title = args[0];

            return true;
        }
    }
}