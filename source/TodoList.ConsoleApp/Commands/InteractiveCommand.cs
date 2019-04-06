namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public sealed class InteractiveCommand : ICommand
    {
        public void Execute(TodoItemsController controller)
        {
            controller.Interactive();
        }
    }
}