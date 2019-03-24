namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Boundaries;
    using TodoList.Core.UseCases;

    public sealed class Presenter:
        IResponseHandler<Core.Boundaries.AddTodoItem.Response>,
        IResponseHandler<Core.Boundaries.ListTodoItems.Response>
        {
            public void Handle(Core.Boundaries.AddTodoItem.Response response)
            {
                Console.WriteLine($"Added {response.Id}.");
            }

            public void Handle(Core.Boundaries.ListTodoItems.Response response)
            {
                foreach (var item in response.Items)
                {
                    if (item.IsCompleted)
                        Console.WriteLine($"{item.Id} [X] {item.Title}.");
                    else
                        Console.WriteLine($"{item.Id} [ ] {item.Title}.");
                }
            }
        }
}