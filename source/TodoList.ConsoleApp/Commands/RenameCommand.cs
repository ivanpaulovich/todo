namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public sealed class RenameCommand : ICommand
    {
        public string Id { get; }
        public string NewTitle { get; }

        public RenameCommand(string id, string newTitle)
        {
            Id = id;
            NewTitle = newTitle;
        }
        
        public void Execute(TodoItemsController controller)
        {
            controller.Execute(this);
        }
    }
}