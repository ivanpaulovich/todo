using System.Collections.ObjectModel;
using TodoList.Core.UseCases;
using TodoList.Core.UseCases.AddTodoItem;
using TodoList.Core.UseCases.ListTodoItems;
using TodoList.WebApi.Models;

namespace TodoList.WebApi.Controllers
{
    public sealed class Presenter : IOutputHandler<AddTodoItemResponse>, IOutputHandler<ListTodoItemsResponse>
    {
        public Collection<TodoItemViewModel> ListItems { get; set; }
        public TodoItemViewModel CreatedItem { get; set; }

        public Presenter()
        {

        }

        public void Handle(AddTodoItemResponse response)
        {
            CreatedItem = new TodoItemViewModel()
            {
                Id = response.Id
            };
        }

        public void Handle(ListTodoItemsResponse response)
        {
            ListItems = new Collection<TodoItemViewModel>();
            foreach(var item in response.Items)
                ListItems.Add(new TodoItemViewModel() { Title = item.Title });
        }
    }
}