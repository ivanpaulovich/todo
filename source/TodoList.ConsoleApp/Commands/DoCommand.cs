namespace TodoList.ConsoleApp.Commands
{
    public sealed class DoCommand : ICommand
    {
        public string Id { get; }

        public DoCommand(string id)
        {
            Id = id;
        }
    }
}