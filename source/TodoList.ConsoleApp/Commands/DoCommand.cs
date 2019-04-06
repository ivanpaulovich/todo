namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public sealed class DoCommand : ICommand
    {
        public string Id { get; }

        public DoCommand(string id)
        {
            Id = id;
        }

        public void Execute(TodoItemsController controller)
        {
            controller.Execute(this);
        }
    }
}