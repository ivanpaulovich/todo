namespace TodoList.ConsoleApp.Commands
{
    public class RemoveCommand : ICommand
    {
        public string Id { get; }

        public RemoveCommand(string id)
        {
            Id = id;
        }
    }
}