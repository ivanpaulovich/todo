namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public interface ICommand
    {
        void Execute(TodoItemsController controller);
    }
}