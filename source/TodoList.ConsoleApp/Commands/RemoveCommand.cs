namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public sealed class RemoveCommand : ICommand
    {
        public string Id { get; }

        public RemoveCommand(string id)
        {
            Id = id;
        }
        public void Execute(TodoItemsController controller)
        {
            controller.Execute(this);
        }
    }
}