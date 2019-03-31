namespace TodoList.ConsoleApp.Commands
{
    public class TodoCommand : ICommand
    {
        public string Title { get; }

        public TodoCommand(string title)
        {
            Title = title;
        }
    }
}