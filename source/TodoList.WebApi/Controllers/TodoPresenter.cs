namespace TodoList.WebApi.Controllers
{
    using System.Collections.ObjectModel;
    using System;
    using Microsoft.AspNetCore.Mvc;
    using TodoList.Core.Boundaries.Todo;
    using TodoList.Core.Boundaries;
    using TodoList.Core.UseCases;
    using TodoList.WebApi.Models;

    public sealed class TodoPresenter : IResponseHandler<Response>
    {
        private Response _response;

        public void Handle(Response response)
        {
            _response = response;
        }

        public ActionResult<TodoItemViewModel> BuildResponse(TodoItemsController todoItemsController)
        {
            if (_response == null)
                return todoItemsController.NotFound();

            if (_response.ItemId == Guid.Empty)
                return todoItemsController.BadRequest();

            var createdItem = new TodoItemViewModel()
            {
                Id = _response.ItemId
            };

            return todoItemsController.CreatedAtAction(nameof(todoItemsController.Post), new { id = createdItem.Id }, createdItem);
        }
    }
}