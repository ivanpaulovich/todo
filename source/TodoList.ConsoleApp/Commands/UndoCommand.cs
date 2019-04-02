namespace TodoList.ConsoleApp.Commands
{
    public sealed class UndoCommand : ICommand
    {
        public string Id { get; }

        public UndoCommand(string id)
        {
            Id = id;
        }
    }
}