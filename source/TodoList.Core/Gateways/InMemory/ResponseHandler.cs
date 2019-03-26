namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TodoList.Core.Boundaries;
    using TodoList.Core.UseCases;

    public sealed class ResponseHandler:
        IResponseHandler<Boundaries.Todo.Response>,
        IResponseHandler<Boundaries.List.Response>
        {
            public Collection<Boundaries.Todo.Response> TodoItems { get; }
            public Collection<Boundaries.List.Response> ListItems { get; }

            public ResponseHandler()
            {
                TodoItems = new Collection<Boundaries.Todo.Response>();
                ListItems = new Collection<Boundaries.List.Response>();
            }

            public void Handle(Boundaries.Todo.Response response)
            {
                TodoItems.Add(response);
            }

            public void Handle(Boundaries.List.Response response)
            {
                ListItems.Add(response);
            }
        }
}