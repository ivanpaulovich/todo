namespace TodoList.Core.Gateways.InMemory
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using TodoList.Core.UseCases;
    using TodoList.Core.Boundaries;

    public sealed class ResponseHandler:
        IResponseHandler<Boundaries.AddTodoItem.Response>,
        IResponseHandler<Boundaries.ListTodoItems.Response>
        {
            public Collection<Boundaries.AddTodoItem.Response> AddTodoItems { get; }
            public Collection<Boundaries.ListTodoItems.Response> ListTodoItems { get; }

            public ResponseHandler()
            {
                AddTodoItems = new Collection<Boundaries.AddTodoItem.Response>();
                ListTodoItems = new Collection<Boundaries.ListTodoItems.Response>();
            }

            public void Handle(Boundaries.AddTodoItem.Response response)
            {
                AddTodoItems.Add(response);
            }

            public void Handle(Boundaries.ListTodoItems.Response response)
            {
                ListTodoItems.Add(response);
            }
        }
}