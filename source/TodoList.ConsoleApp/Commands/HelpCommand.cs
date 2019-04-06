namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public sealed class HelpCommand : ICommand
    {
        public void Execute(TodoItemsController controller)
        {
            controller.Help();
        }
    }
}