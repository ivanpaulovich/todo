namespace TodoList.Core.Boundaries.Todo
{
    public sealed class Request : IRequest
    {
        public string Title { get; }

        public Request(string title)
        {
            Title = title;
        }
    }
}