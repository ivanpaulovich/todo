namespace TodoList.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System;
    using Microsoft.AspNetCore.Mvc;
    using TodoList.Core.Boundaries.List;
    using TodoList.Core.Boundaries;
    using TodoList.Core.UseCases;
    using TodoList.WebApi.Models;

    public sealed class ListPresenter : IResponseHandler<Response>
    {
        private Response _response;

        public void Handle(Response response)
        {
            _response = response;
        }

        public ActionResult<IEnumerable<TodoItemViewModel>> BuildResponse(TodoItemsController todoItemsController)
        {
            if (_response == null)
                return todoItemsController.BadRequest();

            var listItems = new Collection<TodoItemViewModel>();
            foreach (var item in _response.Items)
                listItems.Add(new TodoItemViewModel() { Title = item.Title, Id = item.ItemId });
                
            return listItems;
        }
    }
}