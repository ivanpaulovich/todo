namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public interface ICommand
    {
        bool Match(string[] args);
        void Execute(TodoItemsController controller);
    }
}