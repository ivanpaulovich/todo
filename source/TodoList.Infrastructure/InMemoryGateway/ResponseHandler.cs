namespace TodoList.Infrastructure.InMemoryGateway
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TodoList.Core.Boundaries;
    using TodoList.Core.UseCases;

    public sealed class ResponseHandler : IResponseHandler<Core.Boundaries.List.Response>, IResponseHandler<Core.Boundaries.Todo.Response>
    {
        public Collection<Core.Boundaries.Todo.Response> TodoItems { get; }
        public Collection<Core.Boundaries.List.Response> ListItems { get; }

        public ResponseHandler()
        {
            TodoItems = new Collection<Core.Boundaries.Todo.Response>();
            ListItems = new Collection<Core.Boundaries.List.Response>();
        }

        public void Handle(Core.Boundaries.List.Response response)
        {
            ListItems.Add(response);
        }

        public void Handle(Core.Boundaries.Todo.Response response)
        {
            TodoItems.Add(response);
        }
    }
}