namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public sealed class TodoCommand : ICommand
    {
        public string Title { get; }

        public TodoCommand(string title)
        {
            Title = title;
        }
        
        public void Execute(TodoItemsController controller)
        {
            controller.Execute(this);
        }
    }
}