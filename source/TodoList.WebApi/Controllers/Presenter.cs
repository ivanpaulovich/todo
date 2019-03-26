using System.Collections.ObjectModel;
using TodoList.Core.Boundaries;
using TodoList.Core.UseCases;
using TodoList.WebApi.Models;

namespace TodoList.WebApi.Controllers
{
    public sealed class Presenter:
        IResponseHandler<Core.Boundaries.Todo.Response>,
        IResponseHandler<Core.Boundaries.List.Response>
        {
            public Collection<TodoItemViewModel> ListItems { get; private set; }
            public TodoItemViewModel CreatedItem { get; private set; }

            public void Handle(Core.Boundaries.Todo.Response response)
            {
                CreatedItem = new TodoItemViewModel()
                { 
                    Id = response.ItemId
                };
            }

            public void Handle(Core.Boundaries.List.Response response)
            {
                ListItems = new Collection<TodoItemViewModel>();
                foreach (var item in response.Items)
                    ListItems.Add(new TodoItemViewModel() { Title = item.Title, Id = item.ItemId });
            }
        }
}