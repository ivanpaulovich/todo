namespace TodoList.ConsoleApp.Commands
{
    using TodoList.ConsoleApp.Controllers;

    public sealed class UndoCommand : ICommand
    {
        public string Id { get; }

        public UndoCommand(string id)
        {
            Id = id;
        }
        
        public void Execute(TodoItemsController controller)
        {
            controller.Execute(this);
        }
    }
}