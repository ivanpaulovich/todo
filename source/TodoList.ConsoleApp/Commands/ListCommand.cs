namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public sealed class ListCommand : ICommand
    {
        public void Execute(TodoItemsController controller)
        {
            controller.List();
        }
    }
}