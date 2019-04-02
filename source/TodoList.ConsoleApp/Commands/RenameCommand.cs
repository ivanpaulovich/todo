namespace TodoList.ConsoleApp.Commands
{
    public sealed class RenameCommand : ICommand
    {
        public string Id { get; }
        public string NewTitle { get; }

        public RenameCommand(string id, string newTitle)
        {
            Id = id;
            NewTitle = newTitle;
        }
    }
}