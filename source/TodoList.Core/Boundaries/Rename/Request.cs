namespace TodoList.Core.Boundaries.Rename
{
    public sealed class Request : IRequest
    {
        public Request(string itemId, string title)
        { 
            ItemId = itemId;
            Title = title;
        }

        public string ItemId { get; }
        public string Title { get; }
    }
}