namespace TodoList.Core.UseCases.AddTodoItem
{
    public sealed class Input
    {
        public string Title { get; }

        public Input(string title)
        {
            Title = title;
        }
    }
}