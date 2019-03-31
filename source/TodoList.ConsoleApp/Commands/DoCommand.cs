namespace TodoList.ConsoleApp.Commands
{
    public class DoCommand : ICommand
    {
        public string Id { get; }

        public DoCommand(string id)
        {
            Id = id;
        }
    }
}