namespace TodoList.Core.UseCases.AddTodoItem
{
    public sealed class AddTodoItemRequest
    {
        public string Title { get; }

        public AddTodoItemRequest(string title)
        {
            Title = title;
        }
    }
}